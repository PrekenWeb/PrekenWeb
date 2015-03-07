using Achtergrondtaken.Properties;
using NAudio.FileFormats.Mp3;
using NAudio.Wave;
using Prekenweb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Achtergrondtaken
{
    public class AnalyseerAudioTaak
    {
        internal void Start()
        {
            using (PrekenwebContext _context = new PrekenwebContext())
            {

                foreach (var preek in _context.Preeks.Where(p => p.PreekTypeId != (int)PreekTypeEnum.LeesPreek && !p.Duur.HasValue && !string.IsNullOrEmpty(p.Bestandsnaam) /* && p.Id == 15681*/).ToList())
                {
                    string filename = Path.Combine(Settings.Default.PrekenFolder, preek.Bestandsnaam);
                    if (File.Exists(filename) && Path.GetExtension(filename) == ".mp3")
                    {
                        try
                        {
                            using (Mp3FileReader fr = new Mp3FileReader(filename, (WaveFormat mp3Format) => { return new DmoMp3FrameDecompressor(mp3Format); }))
                            {
                                preek.Duur = fr.TotalTime;
                                preek.Bestandsgrootte = (int)new FileInfo(filename).Length;

                                Console.WriteLine("{0}", preek.GetPreekTitel());
                                _context.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Mislukt: {0} / {1}", preek.GetPreekTitel(), ex.Message);
                        }
                    }
                }
            }
        }
    }
}
