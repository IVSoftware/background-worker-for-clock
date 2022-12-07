namespace background_worker_for_clock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _task = startTask();
        }
        CancellationTokenSource? _cts;
        private Task? _task;

        private async Task startTask()
        {
            var prevSecond = -1;
            _cts = new CancellationTokenSource();
            while(!_cts.IsCancellationRequested) 
            {
                var dt = DateTime.Now;
                if(prevSecond != dt.Second) 
                {
                    prevSecond = dt.Second;
                    BeginInvoke(new Action(() => 
                    {
                        labelClock.Text = dt.ToString("T");
                    }));
                }
                await Task.Delay(1000);
            }
        }

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}