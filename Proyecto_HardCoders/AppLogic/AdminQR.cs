using QRCoder;
using System.IO;
using static QRCoder.QRCodeGenerator;

namespace AppLogic
{

        public class AdminQR
        {
        //https://www.beaconstac.com/what-is-qr-code
        //https://github.com/codebude/QRCoder
        //https://github.com/codebude/QRCoder/wiki/How-to-use-QRCoder
        // https://github.com/JPlenert/QRCoder-ImageSharp
        //https://github.com/JPlenert/QRCoder-ImageSharp/blob/master/QRCoder/QRCodeGenerator.cs



        public byte[] GenerateQR(string informationText)
        {
            var qrGenerator = new QRCodeGenerator();

            var qrCodeData = qrGenerator.CreateQrCode(informationText, ECCLevel.M, true, false);

            var qrCodePng = new PngByteQRCode(qrCodeData);
            //var qrCode = new SvgQRCode(qrCodeData);

            //var re = qrCode.GetGraphic(5);
            var re2 = qrCodePng.GetGraphic(5);

            return re2;
            //return qrCode.GetGraphic(5); //return qrCode.GetGraphic(pixelsPerModule, darkColorHex, lightColorHex, drawQuietZones, sizingMode, logo);
        }








    }

}
