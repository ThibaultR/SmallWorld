using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace UML_SW
{
    [Serializable()]
    public class Map
    {
        public enum mapType { DEMO, SMALL, NORMAL }

        public List<ITile> tilesList { get; set; }

        public StrategyMap strategy { get; set; }

        public Map()
        {
            tilesList = new List<ITile>();
        }
        
        public void setStrategy(mapType strategy)
        {
            switch(strategy){
                case mapType.DEMO:
                    this.strategy = new StrategyDemoMap();
                    break;
                case mapType.SMALL:
                    this.strategy = new StrategySmallMap();
                    break;
                case mapType.NORMAL:
                    this.strategy = new StrategyNormalMap();
                    break;
                default:
                    break;
            }
        }

        unsafe public void createMap()
        {//TODO error if strategy not set
            int n = strategy.size;
            WrapperAlgo algoW = new WrapperAlgo();
            int* tabMap = algoW.mapCreation(n);

            FactoryTile ft = new FactoryTile();
            ITile tempTile;

            for (int i = 0; i < n * n; i++)
            {
                switch (tabMap[i])
                {
                    case 0:
                        tempTile = ft.getOrCreatePlain();
                        break;
                    case 1:
                        tempTile = ft.getOrCreateDesert();
                        break;
                    case 2:
                        tempTile = ft.getOrCreateMountain();
                        break;
                    case 3:
                        tempTile = ft.getOrCreateForest();
                        break;
                    default:
                        tempTile = null; //TODO : error
                        break;
                }
        
                tilesList.Add(tempTile);
            }
        }

        public List<int> convertMapToIntList()
        {
            List<int> listIntMap = new List<int>();
            foreach (ITile t in this.tilesList)
            {
                if (t.GetType() == typeof(Plain))
                {
                    listIntMap.Add(0);
                }
                else if (t.GetType() == typeof(Desert))
                {
                    listIntMap.Add(1);
                }
                else if (t.GetType() == typeof(Mountain))
                {
                    listIntMap.Add(2);
                }
                else if (t.GetType() == typeof(Forest))
                {
                    listIntMap.Add(3);
                }
                else
                {
                    //TODO : error
                }
            }
            return listIntMap;
        }
    }
}
