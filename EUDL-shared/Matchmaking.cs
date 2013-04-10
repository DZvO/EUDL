using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUDL_shared {
	public class Matchmaking {
		/** Calculates new Elo rating
		 * Ra is the current ranking of the player we want to calculate a new rating for.
		 * RB is the opponents ranking.
		 * Sa determines wether our player won (1), lost (0), or got a draw(0.5)
		 * K is a factor determining how big of a change the elo score will experience.
		 */
		public float CalculateNewElo(float Ra, float Rb, float Sa, float K) {
			float Ea = (float)(1.0 / (1.0 + Math.Pow(10, (Rb - Ra) / 400.0)));
			float Raa = Ra + K * (Sa - Ea);
			return Raa;
		}
	}
}
