using System.Collections.Generic;
using System.Threading.Tasks;
using Eto.Forms;
using MailApp.Core.Service;
using EtoMailApp.UI.EtoPortable.View;

namespace EtoMailApp.Desktop.Service
{
    public class EtoDialogService : IDialogService
    {
        public IEnumerable<string> OpenFiles()
        {
            using (var fileDialog = new OpenFileDialog())
                return fileDialog.ShowDialog(Owner) == DialogResult.Ok ? fileDialog.Filenames : null;
        }
        public Task<IEnumerable<string>> OpenFilesAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<string>>();
            Application.Instance.AsyncInvoke(() =>
            {
                var res = OpenFiles();
                tcs.SetResult(res);
            });
            return tcs.Task;
        }
        public string SaveFile()
        {
            using (var fileDialog = new SaveFileDialog())
                return fileDialog.ShowDialog(Owner) == DialogResult.Ok ? fileDialog.FileName : null;
        }
        public Task<string> SaveFileAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            Application.Instance.AsyncInvoke(() =>
            {
                var res = SaveFile();
                tcs.SetResult(res);
            });
            return tcs.Task;
        }
        public void ErrorAlarm(string text, string caption = "")
        {
            MessageBox.Show(Owner, text, caption, MessageBoxType.Error);
        }
        public Task ErrorAlarmAsync(string text, string caption = "")
        {
            var tcs = new TaskCompletionSource<bool>();
            Application.Instance.AsyncInvoke(() =>
            {
                ErrorAlarm(text, caption);
                tcs.SetResult(true);
            });
            return tcs.Task;
        }
        public string OpenRecipient()
        {
			using (var recepientForm = new RecepientForm())
            {
                recepientForm.ShowModal(Owner);
                return recepientForm.OkDialogResult ? recepientForm.Email : null;
            }
        }
        public async Task<string> OpenRecipientAsync()
        {
            using (var recepientForm = new RecepientForm())
            {
				await recepientForm.ShowModalAsync(Owner);
                return recepientForm.OkDialogResult ? recepientForm.Email : null;
            }
        }

        public Control Owner { get; set; }
    }
}