using BlockchainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainProject.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var Result = new List<string>();
			const string minerAddress = "miner1";
			const string user1Address = "A";
			const string user2Address = "B";
			BlockChain blockChain = new BlockChain() { ProofOfWorkDifficulty = 2, MiningReward = 10};
			blockChain.CreateTransaction(new Transaction() { From = user1Address, To = user2Address, Amount = 200 });
			blockChain.CreateTransaction(new Transaction() { From = user2Address, To = user1Address, Amount = 10 });
			Result.Add(string.Format("Est valide: {0}", blockChain.IsValidChain()));
			Result.Add("--------- Démarrer l'extraction ---------");
			blockChain.MineBlock(minerAddress);
			Result.Add(string.Format("ÉQUILIBRE du mineur: {0}", blockChain.GetBalance(minerAddress)));
			blockChain.CreateTransaction(new Transaction() { From = user1Address, To = user2Address, Amount = 5 });
			Result.Add("--------- Démarrer l'extraction ---------");
			blockChain.MineBlock(minerAddress);
			Result.Add(string.Format("ÉQUILIBRE du mineur: {0}", blockChain.GetBalance(minerAddress)));
			Result.AddRange(PrintChain(blockChain,Result));
			Result.Add("Piratage de la blockchain ...");
			blockChain.Bloks[1].Transactions = new List<Transaction>  { new Transaction() { From = user1Address, To = minerAddress, Amount = 150 } };
			Result.Add(string.Format("Est valide: {0}", blockChain.IsValidChain()));
			Result.Add("----------------- End Blockchain -----------------");
			ViewBag.Result = Result;
			return View();
		}




		private List<string> PrintChain(BlockChain blockChain, List<string> result)
		{
			result.Add(string.Format("----------------- Démarrer la chaîne de caractères ----------------- "));
			foreach (Block block in  blockChain.Bloks) 
			{
				result.Add(string.Format("------ Bloc de démarrage ------"));
				result.Add(string.Format("Hash: {0}", block.Hash));
				result.Add(string.Format("Hachage précédent: {0}", block.PreviousHash));
				result.Add("--- --- Start Transactions ---");
				foreach (Transaction transaction in   block.Transactions) 
				{
					result.Add(string.Format("De: {0} au {1} montant {2}", transaction.From, transaction.To, transaction.Amount));
				}
				result.Add("--- Terminer les transactions ---");
				result.Add("----------------- End Block -----------------");
			}
			return result;
		}



		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}