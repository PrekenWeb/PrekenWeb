namespace Prekenweb.Models.Dtos
{
	public class Zoekopdracht
	{
		public class ZoekOpdracht  
		{  
			public bool LeesPreken { get; set; }
			public bool AudioPreken { get; set; }
			public bool Lezingen { get; set; }

			public int? PredikantId { get; set; }  
			public int? BoekHoofdstukId { get; set; }  
			public int? BoekId { get; set; } 
			public int? LezingCategorieId { get; set; }  
			public int? HoofdstukId { get; set; } 
			public int? GebeurtenisId { get; set; } 
			public int? GemeenteId { get; set; }  
			public int? SerieId { get; set; }  
			public int TaalId { get; set; } 

			public string Zoekterm { get; set; }

			public bool SorteertAscending { get; set; } 
		}
	}
}

