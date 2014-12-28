using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Map
    {
        public enum mapType { DEMO, SMALL, NORMAL }

        public List<ITile> tilesList
        {
            get;
            set;
        }

        public StrategyMap strategy
        {
            get;
            set;
        }

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

        public Map createMap()
        {
            throw new System.NotImplementedException();
        }
    }
}
