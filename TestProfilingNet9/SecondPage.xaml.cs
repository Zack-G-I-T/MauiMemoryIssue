namespace TestProfilingNet9
{
    public partial class SecondPage : ContentPage, IDisposable
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void DisposePage(Microsoft.Maui.Controls.Page? page)
        {
            if (page != null)
            {
                DisposeChildElements(page);
                if (page.BindingContext is IDisposable disposableViewModel)
                {
                    disposableViewModel.Dispose();
                }
                page.BindingContext = null;
            }
        }

        private void DisposeChildElements(IView view)
        {
            if (view is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    DisposeChildElements(child);
                    if (child is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
            else if (view is IContentView { Content: View viewContent })
            {
                DisposeChildElements(viewContent);
            }

            if (view is IDisposable disposableView)
            {
                disposableView.Dispose();
            }
        }

        public virtual void Dispose()
        {
            this.Behaviors.Clear();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisposePage(this);
            Shell.Current.GoToAsync("..");
            GC.Collect();
        }
    }

}
