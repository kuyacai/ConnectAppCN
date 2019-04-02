using Unity.UIWidgets.foundation;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace ConnectApp.components.pull_to_refresh {
    public class SmartRefreshFooter : StatelessWidget {
        public readonly int mode;
        public readonly EdgeInsets padding;

        public SmartRefreshFooter(
            int mode,
            EdgeInsets padding = null,
            Key key = null) : base(key) {
            this.mode = mode;
            this.padding = padding ?? EdgeInsets.symmetric(vertical: 20.0f);
        }


        public override Widget build(BuildContext context) {
            AnimatingType animatingType = AnimatingType.stop;
            if (mode == 2) animatingType = AnimatingType.repeat;
            if (mode == 3) animatingType = AnimatingType.stop;
            if (mode == 0) animatingType = AnimatingType.reset;
            return new Container(
                padding: padding,
                child: new CustomActivityIndicator(
                    animating: animatingType,
                    size: 24
                )
            );
        }
    }
}