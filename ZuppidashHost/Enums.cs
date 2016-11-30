using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuppidashHost
{
    public static class Enums
    {
        public enum CommandTargets { DISPLAY, IRACING, PROGRAM };
        public enum Commands { DISPLAY_NEXT, DISPLAY_PREVIOUS, DISPLAY_CLEAR, IR_NOTIRES, IR_ALLTIRES};
        public enum SessionState { INVALID, GRIDDING, WARMUP, PARADE, RACING, CHECKERED, COOLDOWN };
        public enum TrackLoc { NOTINWORLD, OFFTRACK, PITSTALL, PITTING, ONTRACK};
        public enum DisplayMode { DEFAULT,TIMELEFT, TCCHANGE, BBCHANGE, GEARCHANGE };
    }
}
