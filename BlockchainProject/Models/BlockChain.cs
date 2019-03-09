using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainProject.Models
{
	public class BlockChain
	{
		public int ProofOfWorkDifficulty { get; set; }
		public  double MiningReward { get; set; }
		public  List<Transaction> PendingTransactions { get; set; }
		public  List<Block> Bloks { get; set; }

		public BlockChain()
		{
			this.PendingTransactions = new List<Transaction>();
			this.Bloks = new List<Block> { CreateGenisBlock() };
		}

	

		public void CreateTransaction(Transaction transaction)
		{
			this.PendingTransactions.Add(transaction);
		}

		public void MineBlock(string minerAddress)
		{
			Transaction minerRewardTransaction = new Transaction()
			{
				Amount = this.MiningReward,
				From = null,
				To = minerAddress
			};

			//this.PendingTransactions.Add(minerRewardTransaction);
			CreateTransaction(minerRewardTransaction);

			Block block = new Block()
			{
				Transactions = PendingTransactions
			};

			block.MinBlock(this.ProofOfWorkDifficulty);

			block.PreviousHash = Bloks.Last().Hash;
			Bloks.Add(block);

			this.PendingTransactions = new List<Transaction>();
		}

		public bool IsValidChain()
		{
			for (int i = 1; i < this.Bloks.Count; i++)
			{
				Block previousBlock = this.Bloks[i - 1];
				Block currentBlock = this.Bloks[i];

				if (currentBlock.Hash != currentBlock.CreateHash())
					return false;

				if (currentBlock.PreviousHash != previousBlock.Hash)
					return false;
			}

			return true;
		}
		public double GetBalance(string address)
		{
			double balance = 0;

			foreach (var block in this.Bloks)
			{
				foreach (var transaction in block.Transactions)
				{
					if (transaction.From == address)
						balance -= transaction.Amount;

					if (transaction.To == address)
						balance += transaction.Amount;
				}
			}

			return balance;
		}


		public Block CreateGenisBlock()
		{
			var transactions = new List<Transaction>()
			{
				new Transaction()
				{
					From = "",
					To = "",
					Amount = 0
				}
			};

			return new Block()
			{
				Transactions = transactions,
				PreviousHash = "00000"
			};
		}
	}
}