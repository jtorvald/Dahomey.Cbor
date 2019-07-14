using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dahomey.Cbor.Cwt.Tests
{
    [TestClass]
    public class CwtTest
    {
        [TestMethod]
        [DataRow("d83dd18443a10104a1044c53796d6d65747269633235365850a70175636f61703a2f2f61732e6578616d706c652e636f6d02656572696b77037818636f61703a2f2f6c696768742e6578616d706c652e636f6d041a5612aeb0051a5610d9f0061a5610d9f007420b7148093101ef6d789200", "a4205820403697de87af64611c1d32a05dab0fe1fcb715a86ab435f1ec99192d795693880104024c53796d6d65747269633235360304")]
        public void CwtValidation(string cwt, string key)
        {
            bool isValid = Cwt.ValidateToken(cwt.HexToBytes(), key.HexToBytes());
            Assert.IsTrue(isValid);
        }
    }
}
