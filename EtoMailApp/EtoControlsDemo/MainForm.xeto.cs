using System;
using Eto.Forms;
using Eto.Drawing;
using Eto;
using Eto.Serialization.Xaml;

namespace EtoControlsDemo
{
    public class MainForm : Form
    {
        public MainForm()
        {
            
            //Eto.Style.Add<Button>("btnDemo", btnDemo => btnDemo.Image = Bitmap.FromResource("EtoControlsDemo.icon.png"));
            Eto.Style.Add<Drawable>("drawable", drawable =>
            {
                //if(drawable.SupportsCreateGraphics)
                    //using (var graphics = drawable.CreateGraphics())
                      //  graphics.RotateTransform(90);
            });
            Eto.Style.Add<ImageView>("imageView", imageView => imageView.Image = Bitmap.FromResource("EtoControlsDemo.icon.png"));
            XamlReader.Load(this);
        }

        protected void HandleClickMe(object sender, EventArgs e)
        {
            MessageBox.Show("I was clicked!");
        }

        protected void HandleQuit(object sender, EventArgs e)
        {
            Application.Instance.Quit();
        }
    }
}
