using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public static class Factors
	{
		public static Dictionary<int, int> PrimeFactorize(int value)
		{
			List<int> GetPrimes(int max)
			{
				var primeList = new List<int>();
				var sieve = new HashSet<int>();
				for (var prime = 2; prime <= max; prime++)
				{
					if (!sieve.Contains(prime))
					{
						primeList.Add(prime);
						for (var i = prime * 2; i <= max; i += prime)
							sieve.Add(i);
					}
				}
				return primeList;
			}

			var values = new Dictionary<int, int>();
			var start = (value / 2);
			var primes = GetPrimes(start);
			for (var i = 0; i < primes.Count; i++)
			{
				var prime = primes[i];
				var count = 0;
				while (value % prime == 0)
				{
					count++;
					value /= prime;
				}
				if (count > 0)
					values[prime] = count;
			}
			//	If we got no primes, our value is prime
			if (values.Count == 0)
				values[value] = 1;
			return values;

		}
		public static int[] GetRatio(int valueA, int valueB)
		{
			var fa = PrimeFactorize(valueA);
			var fb = PrimeFactorize(valueB);

			//	Remove the primes in common
			void Reconcile(Dictionary<int, int> da, Dictionary<int, int> db)
			{
				da.Keys.ToList().ForEach(a =>
				{
					while (da[a] > 0 && db.ContainsKey(a) && db[a] > 0)
					{
						da[a]--;
						db[a]--;
					}
					if (da[a] == 0)
						da.Remove(a);
					if (db.ContainsKey(a) && db[a] == 0)
						db.Remove(a);
				});
			}

			Reconcile(fa, fb);
			Reconcile(fb, fa);

			//	Total up the remaining values
			return new[]
			{
				fa.Aggregate(1, (current, next) => current *= (int)Math.Pow(next.Key, next.Value)),
				fb.Aggregate(1, (current, next) => current *= (int)Math.Pow(next.Key, next.Value))
			};
		}
	}
}
