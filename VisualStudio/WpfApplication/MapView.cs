
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wrapper;


namespace WpfApplication
{
    class MapView : Panel
    {
        public const int TAILLE = 6;//TODO taille

        unsafe protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {

            WrapperAlgo algoW = new WrapperAlgo();
             int * tabmap = algoW.mapCreation(TAILLE);//TODO use the good truc

            
            BitmapImage img0 = new BitmapImage(new Uri("textures/plain.png", UriKind.Relative));
            BitmapImage img1 = new BitmapImage(new Uri("textures/desert.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri("textures/mountain.png", UriKind.Relative));
            BitmapImage img3 = new BitmapImage(new Uri("textures/forest.png", UriKind.Relative));

            BitmapImage[] tabimg = {img0, img1, img2, img3};

            for (int i = 0; i < TAILLE; i++ )
            {
                for (int j = 0; j < TAILLE; j++)
                {
                    int tile = tabmap[j * TAILLE + i];



                    double posx = i * tabimg[tile].PixelWidth;
                    double posy = j * tabimg[tile].PixelHeight * 3 / 4;
                    if(j % 2 == 1 ){
                        posx += tabimg[tile].PixelWidth / 2;
                    }
                    dc.DrawImage(tabimg[tile], new Rect(posx, posy, tabimg[tile].PixelWidth, tabimg[tile].PixelHeight));
                }
            }
            
        }
    }
}
