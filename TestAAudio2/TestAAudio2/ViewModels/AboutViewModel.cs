using System;
using TestAAudio.Services;
using TestAAudio2;
using Xamarin.Forms;

namespace TestAAudio.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        private IAudioService audioService = DependencyService.Get<IAudioService>();
        //private IntPtr inputStream;
        //private IntPtr outputStream;
        private bool showError;
        private bool showStatus;
        private string errorMessage;
        private bool creatingAudioStreams;
        private StreamsData streamData;

        public AaudioStreamCreateParamsViewModel InputCreateParams { get; private set; }
        public AaudioStreamCreateParamsViewModel OutputCreateParams { get; private set; }
        public AaudioStreamStatusViewModel InputStatus { get; private set; }
        public AaudioStreamStatusViewModel OutputStatus { get; private set; }

        public bool ShowError
        {
            get { return showError; }
            set { SetProperty(ref showError, value); }
        }

        public bool ShowStatus
        {
            get { return showStatus; }
            set { SetProperty(ref showStatus, value); }
        }

        public bool CreatingAudioStreams
        {
            get { return creatingAudioStreams; }
            set { SetProperty(ref creatingAudioStreams, value); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public AboutViewModel()
        {
            CreateStreams = new Command(DoCreateStreams, CanCreateStreams);
            CloseStreams = new Command(DoCloseStreams, CanCloseStreams);
            InputCreateParams = new AaudioStreamCreateParamsViewModel();
            OutputCreateParams = new AaudioStreamCreateParamsViewModel();
            InputStatus = new AaudioStreamStatusViewModel();
            OutputStatus = new AaudioStreamStatusViewModel();
        }

        private bool CanCloseStreams()
        {
            return streamData != null;
        }

        private void DoCloseStreams()
        {
            if (streamData != null && streamData.StreamCalbacks != null)
            {
                streamData.StreamCalbacks.Dispose();
                streamData = null;
            }
            //audioService.CloseStream(inputStream);
            //audioService.CloseStream(outputStream);
            //inputStream = IntPtr.Zero;
            //outputStream = IntPtr.Zero;
            InputStatus.Reset();
            OutputStatus.Reset();
            CreateStreams.ChangeCanExecute();
            CloseStreams.ChangeCanExecute();
        }

        private bool CanCreateStreams()
        {
            //return !InputStatus.Created && !OutputStatus.Created && !CreatingAudioStreams;
            return streamData == null && !CreatingAudioStreams;
        }

        private void DoCreateStreams()
        {
            ShowError = false;
            ShowStatus = false;
            CreatingAudioStreams = true;
            CreateStreams.ChangeCanExecute();
            CloseStreams.ChangeCanExecute();

            App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    streamData = audioService.CreateStreams(InputCreateParams.ToParams(), OutputCreateParams.ToParams());
                    InputStatus.FromStatus(streamData.InputStreamStatus);
                    OutputStatus.FromStatus(streamData.OutputStreamStatus);
                    ShowStatus = true;
                }
                catch (Exception ex)
                {
                    DoCloseStreams();
                    ErrorMessage = ex.Message;
                    ShowError = true;
                    ShowStatus = false;
                }

                CreatingAudioStreams = false;

                CreateStreams.ChangeCanExecute();
                CloseStreams.ChangeCanExecute();
            });
        }

        //private void DoCreateStreams_old()
        //{
        //    ShowError = false;
        //    ShowStatus = false;
        //    CreatingAudioStreams = true;
        //    CreateStreams.ChangeCanExecute();
        //    CloseStreams.ChangeCanExecute();

        //    App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
        //    {
        //        try
        //        {
        //            inputStream = audioService.CreateStream(StreamDirection.Input, InputCreateParams.ToParams());
        //            outputStream = audioService.CreateStream(StreamDirection.Output, OutputCreateParams.ToParams());
        //            InputStatus.FromStatus(audioService.GetStreamStatus(inputStream));
        //            OutputStatus.FromStatus(audioService.GetStreamStatus(outputStream));
        //            ShowStatus = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            DoCloseStreams();
        //            ErrorMessage = ex.Message;
        //            ShowError = true;
        //            ShowStatus = false;
        //        }

        //        CreatingAudioStreams = false;

        //        CreateStreams.ChangeCanExecute();
        //        CloseStreams.ChangeCanExecute();
        //    });
        //}

        public Command CreateStreams { get; }
        public Command CloseStreams { get; }
    }
}