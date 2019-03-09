using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BlockchainProject.Models
{
	public class Block
	{
		public long nonce { get; private set; }
		public DateTime timeStamp { get; }
		public string PreviousHash { get; set; }
		public string Hash { get; private set; }
		public List<Transaction> Transactions { get; set; }

		public Block()
		{
			this.timeStamp = DateTime.Now;
			this.nonce = 0;
			Hash = CreateHash();
		}
		
		public string MinBlock (int proofofWorkDifficulty)
		{
			string hashValidationTemplate = new string('0', proofofWorkDifficulty);
			while(Hash.Substring(0,proofofWorkDifficulty) != hashValidationTemplate)
			{
				this.nonce++;
				Hash = CreateHash();
			}

			return string.Format("Bloqué avec HASH = {0} extrait avec succès!", Hash);
		}

		public string CreateHash()
		{
			using(SHA256 sha256 = SHA256.Create()){
				var rawData = PreviousHash + timeStamp + Transactions + nonce;
				var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				return Encoding.Default.GetString(bytes);
			}
		}
	}
}