using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_check
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] img_path_petit = new string[]
            {
                "Directory.GetCurrentDirectory() + @"\ress\test.jpg"",
                "",
                "",
                "",
                "",
            };
        }

        /* Class regroupant les fonctions permettant de chercher une image dans une autre par comparaison de pixel */
        public Boolean checkImagePresentInImage(string screen_check, string img_check)
        {
            int i = 0;
            bool trouve = false;
            System.Drawing.Bitmap sourceImage = (Bitmap)Bitmap.FromFile(screen_check);
            System.Drawing.Bitmap template = (Bitmap)Bitmap.FromFile(img_check);
            
			// create template matching algorithm's instance (hreshold ~ 92.5%)
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.921f);
			
            // find all matchings with specified above similarity
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, template);
			
            // highlight found matchings
            System.Drawing.Imaging.BitmapData data = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadWrite, sourceImage.PixelFormat);

            foreach (TemplateMatch m in matchings)
            {
                // on compte le nombre de tour dans le foreach(1 tour = image trouvée, 0 = image non trouvée)
                i++;
                AForge.Imaging.Drawing.Rectangle(data, m.Rectangle, System.Drawing.Color.White);
                //affiche la localisation de l'image trouvée
                //MessageBox.Show("position= " + m.Rectangle.Location.ToString());
            }
            // à revoir pour faire chaque perso de chaque groupe
            if (i != 0)
            {
                trouve = true;
            }
            sourceImage.UnlockBits(data);

            return trouve;
        }

        /* Class regroupant les fonctions permettant de chercher une image  dans un Stream Image par comparaison de pixel */
        public Boolean checkImagePresentInStreamImage(MemoryStream screen_check, string img_check)
        {
            int i = 0;
            bool trouve = false;
            System.Drawing.Bitmap sourceImage = (Bitmap)Bitmap.FromStream(new MemoryStream(screen_check.ToArray()));
            System.Drawing.Bitmap template = (Bitmap)Bitmap.FromFile(img_check);
            // System.Drawing.Bitmap template = (Bitmap)Properties.Resources.r_egide; // avec les images en resources(not working)

            // create template matching algorithm's instance (hreshold ~ 92.5%)
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.921f);
			
            // find all matchings with specified above similarity
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, template);
			
            // highlight found matchings
            System.Drawing.Imaging.BitmapData data = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadWrite, sourceImage.PixelFormat);

            foreach (TemplateMatch m in matchings)
            {
                // on compte le nombre de tour dans le foreach(1 tour = image trouvée, 0 = image non trouvée)
                i++;
                AForge.Imaging.Drawing.Rectangle(data, m.Rectangle, System.Drawing.Color.White);
                //affiche la localisation de l'image trouvée
                //MessageBox.Show("position= " + m.Rectangle.Location.ToString());
            }
            // à revoir pour faire chaque perso de chaque groupe
            if (i != 0)
            {
                trouve = true;
            }
            sourceImage.UnlockBits(data);

            return trouve;
        }

        /* Check Toute les images */
        public void checkAllImage(string screen_check, string[] tab_img_path)
        {
            for(int i=0;i<tab_img_path.Length; i++)
            {
                Debug.WriteLine("img: " + tab_img_path[i] + " present ? " + checkImagePresentInImage(screen_check, tab_img_path[i]));
            }
        }
    }
    
}
