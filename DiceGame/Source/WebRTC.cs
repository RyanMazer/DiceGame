//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.MixedReality.WebRTC;

//namespace DiceGame.Source
//{
//    public class WebRTC
//    {
//        PeerConnection _peerConnection;

//        public  WebRTC()
//        {
//            _peerConnection = new PeerConnection();
//        }

//        public async Task Connect()
//        {
//            var config = new PeerConnectionConfiguration
//            {
//                IceServers = new List<IceServer> { new IceServer { Urls = { "stun:stun.l.google.com:19302" } } }
//            };

//            await _peerConnection.InitializeAsync(config);
//        }

//        public void CloseConnection()
//        {
//            _peerConnection.Close();
//            _peerConnection.Dispose();
//            _peerConnection = null;
//        }
//    }
//}
