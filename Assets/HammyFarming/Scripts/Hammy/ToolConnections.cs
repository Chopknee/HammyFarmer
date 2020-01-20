using UnityEngine;

namespace HammyFarming.Hammy {

    public class ToolConnections: MonoBehaviour {

        public HammyFarming.Brian.ToolAttachment connectedTool;

        public delegate void ConnectHammy( HammyFarming.Brian.ToolAttachment connection );
        public ConnectHammy OnHammyConnected;

        public delegate void DisconnectHammy ( HammyFarming.Brian.ToolAttachment connection );
        public DisconnectHammy OnHammyDisconnected;

    }
}