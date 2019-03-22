using Nethereum.Web3.Accounts.Managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.RPC.Accounts;
using System.IO;
using Nethereum.Web3.Accounts;

namespace Nethereum.Tutorials
{
    [TestClass]
    public class Deployment
    {
        [TestMethod]
        public async Task ShouldBeAbleToDeployAContract()
        {
            /*var senderAddress = "0xacF5dc45172202D2Ed9721AA48030A9d0FB0f0b8";
            var password = "123qwepo!";*/
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            senderAddress = "0xacF5dc45172202D2Ed9721AA48030A9d0FB0f0b8";
            var password = "password";
            var abi = @"[{'constant':false,'inputs':[{'name':'val','type':'int256'}],'name':'multiply','outputs':[{'name':'d','type':'int256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[{'name':'multiplier','type':'int256'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'}]";
            var byteCode =
                "0x6060604052341561000f57600080fd5b6040516020806100f283398101604052808051906020019091905050806000819055505060b1806100416000396000f300606060405260043610603f576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631df4f144146044575b600080fd5b3415604e57600080fd5b606260048080359060200190919050506078565b6040518082815260200191505060405180910390f35b60008054820290509190505600a165627a7a723058200dfd1138ee1b70e240253d56f9253b3c82bc9f1058e3cb18c2cf7a86691d60b60029";
            
            var multiplier = 7;
            /* Localhost
            var account = new ManagedAccount(senderAddress, password);*/

            var privateKey = "0x410A096CB9A795B5AF3BC77008916B5B42DF4BC7F2C07F882E01042ECED1437D";
            var account = new Account(privateKey);

            var web3 = new Geth.Web3Geth(account, "https://ropsten.infura.io/v3/1fd409b1b2df4a6cae70090e46ec91c5");


            try
            {
                var receipt =
                    await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byteCode, senderAddress, new Hex.HexTypes.HexBigInteger(900000), null, multiplier);
           

            var contractAddress = receipt.ContractAddress;

            var contract = web3.Eth.GetContract(abi, contractAddress);

            var multiplyFunction = contract.GetFunction("multiply");

            var result = await multiplyFunction.CallAsync<int>(7);
            File.WriteAllText("d:\\eth.txt",""+result);
            Console.WriteLine(result+"");
            Assert.AreEqual(49, result);
            }
            catch (Exception e)
            {
                File.WriteAllText("D:\\h.txt", e.ToString());
                Console.Read();
            }
        }
    }
}
