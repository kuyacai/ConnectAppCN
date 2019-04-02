using System.Collections.Generic;
using ConnectApp.constants;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace ConnectApp.components.pull_to_refresh {
    public enum IconPosition {
        left,
        right,
        top,
        bottom
    }

    public class ClassicIndicator : Indicator {
        public ClassicIndicator(
            int mode,
            string releaseText = null,
            string idleText = null,
            string refreshingText = null,
            string completeText = null,
            string failedText = null,
            string noDataText = null,
            Widget releaseIcon = null,
            Widget idleIcon = null,
            Widget noMoreIcon = null,
            Widget refreshingIcon = null,
            Widget completeIcon = null,
            Widget failedIcon = null,
            float height = 60,
            float spacing = 15,
            IconPosition iconPos = IconPosition.left,
            TextStyle textStyle = null,
            Key key = null
        ) : base(mode, key) {
            this.releaseText = releaseText ?? "";
            this.idleText = idleText ?? "";
            this.refreshingText = refreshingText ?? "";
            this.completeText = completeText ?? "";
            this.failedText = failedText ?? "";
            this.noDataText = noDataText ?? "";
            this.releaseIcon = releaseIcon ?? new Icon(Icons.settings);
            this.idleIcon = idleIcon ?? new Icon(Icons.close);
            this.noMoreIcon = noMoreIcon ?? new Icon(Icons.ellipsis);
            this.refreshingIcon = refreshingIcon ?? new CustomActivityIndicator();
            this.completeIcon = completeIcon ?? new Icon(Icons.favorite);
            this.failedIcon = failedIcon;
            this.height = height;
            this.spacing = spacing;
            this.iconPos = iconPos;
            this.textStyle = textStyle;
        }


        public readonly string releaseText;
        public readonly string idleText;
        public readonly string refreshingText;
        public readonly string completeText;
        public readonly string failedText;
        public readonly string noDataText;
        public readonly Widget releaseIcon;
        public readonly Widget idleIcon;
        public readonly Widget refreshingIcon;
        public readonly Widget completeIcon;
        public readonly Widget failedIcon;
        public readonly Widget noMoreIcon;
        public readonly float height;
        public readonly float spacing;
        public readonly IconPosition iconPos;
        public readonly TextStyle textStyle;


        public override State createState() {
            return new _ClassicIndicatorState();
        }
    }


    internal class _ClassicIndicatorState : State<ClassicIndicator> {
        public override Widget build(BuildContext context) {
            Widget textWidget = _buildText();
            Widget iconWidget = _buildIcon();
            var children = new List<Widget> {
                iconWidget,
                new Container(
                    width: widget.spacing,
                    height: widget.spacing
                ),
                textWidget
            };
            Widget container = new Row(
                textDirection: widget.iconPos == IconPosition.right
                    ? TextDirection.rtl
                    : TextDirection.ltr,
                mainAxisAlignment: MainAxisAlignment.center,
                children: children
            );
            if (widget.iconPos == IconPosition.top ||
                widget.iconPos == IconPosition.bottom)
                container = new Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    verticalDirection: widget.iconPos == IconPosition.top
                        ? VerticalDirection.down
                        : VerticalDirection.up,
                    children: children
                );

            return new Container(
                alignment: Alignment.center,
                height: widget.height,
                child: new Center(
                    child: container
                )
            );
        }


        private Widget _buildText() {
            return new Text(
                widget.mode == RefreshStatus.canRefresh
                    ? widget.releaseText
                    : widget.mode == RefreshStatus.completed
                        ? widget.completeText
                        : widget.mode == RefreshStatus.failed
                            ? widget.failedText
                            : widget.mode == RefreshStatus.refreshing
                                ? widget.refreshingText
                                : widget.mode == RefreshStatus.noMore
                                    ? widget.noDataText
                                    : widget.idleText,
                style: widget.textStyle);
        }

        private Widget _buildIcon() {
            Widget icon = widget.mode == RefreshStatus.canRefresh
                ? widget.releaseIcon
                : widget.mode == RefreshStatus.noMore
                    ? widget.noMoreIcon
                    : widget.mode == RefreshStatus.idle
                        ? widget.idleIcon
                        : widget.mode == RefreshStatus.completed
                            ? widget.completeIcon
                            : widget.mode == RefreshStatus.failed
                                ? widget.failedIcon
                                : new SizedBox(
                                    width: 24,
                                    height: 24,
                                    child: widget.refreshingIcon
                                );
            return icon;
        }
    }
}