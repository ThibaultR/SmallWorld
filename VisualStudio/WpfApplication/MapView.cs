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
using UML_SW;


namespace WpfApplication
{
    class MapView : Panel
    {
        private Game game;
        private List<int> listIntMap;

        public MapView(Game g){
            this.game = g;
            this.listIntMap = new List<int>();
            listIntMap = this.game.map.convertMapToIntList();
        }


        unsafe protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            int TAILLE = this.game.map.strategy.size;
            
            BitmapImage img0 = new BitmapImage(new Uri("textures/tile/plain.png", UriKind.Relative));
            BitmapImage img1 = new BitmapImage(new Uri("textures/tile/desert.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri("textures/tile/mountain.png", UriKind.Relative));
            BitmapImage img3 = new BitmapImage(new Uri("textures/tile/forest.png", UriKind.Relative));

            BitmapImage[] tabimg = {img0, img1, img2, img3};

            for (int j = 0; j < TAILLE; j++ )
            {
                for (int i = 0; i < TAILLE; i++)
                {
                    int tile = listIntMap[j * TAILLE + i];

                    double d = tabimg[tile].PixelWidth / 2 * Math.Tan(30 * Math.PI / 180);

                    double posx = i * tabimg[tile].PixelWidth;
                    double posy = j * (tabimg[tile].PixelHeight - d);
                    if(j % 2 == 1 ){
                        posx += tabimg[tile].PixelWidth / 2;
                    }

                    dc.DrawImage(tabimg[tile], new Rect(posx, posy, tabimg[tile].PixelWidth, tabimg[tile].PixelHeight));
                }
            }
            
        }
    }
}
