using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ProcessorInfoController : ControllerBase
    {
        private readonly ILogger<ProcessorInfoController> _logger;

        public static ProcessInfos? GetProcessInfo(int pid)
        {
            string statusFile = $"/proc/{pid}/status";
            string smapsFile = $"/proc/{pid}/smaps";

            var processInfo = new ProcessInfos();

            try
            {
                // Ler informações do arquivo status
                foreach (var line in System.IO.File.ReadLines(statusFile))
                {
                    if (line.StartsWith("Name:"))
                        processInfo.Name = line.Split('\t')[1].Trim();
                    else if (line.StartsWith("State:"))
                        processInfo.State = line.Split('\t')[1].Trim();
                    else if (line.StartsWith("Threads:"))
                        processInfo.Threads = int.Parse(line.Split('\t')[1].Trim());
                    else if (line.StartsWith("VmSize:"))
                        processInfo.VmSize = int.Parse(line.Split()[1]); // kB
                    else if (line.StartsWith("VmRSS:"))
                        processInfo.VmRSS = int.Parse(line.Split()[1]);  // kB
                    else if (line.StartsWith("VmData:"))
                        processInfo.VmData = int.Parse(line.Split()[1]); // kB
                    else if (line.StartsWith("VmStk:"))
                        processInfo.VmStk = int.Parse(line.Split()[1]);  // kB
                    else if (line.StartsWith("VmExe:"))
                        processInfo.VmExe = int.Parse(line.Split()[1]);  // kB
                }

                // Calcular o número total de páginas (VmRSS / 4 KB)
                if (processInfo.VmRSS.HasValue)
                {
                    processInfo.TotalPages = processInfo.VmRSS / 4; // Tamanho da página geralmente 4 KB
                }

                // Ler informações do arquivo smaps para Pss_Total
                int pssTotal = 0;
                foreach (var line in System.IO.File.ReadLines(smapsFile))
                {
                    if (line.StartsWith("Pss:"))
                    {
                        pssTotal += int.Parse(line.Split()[1]); // kB
                    }
                }
                processInfo.PssTotal = pssTotal;

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Process {pid} not found or inaccessible.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading process {pid}: {ex.Message}");
                return null;
            }

            return processInfo;
        }

        public ProcessorInfoController(ILogger<ProcessorInfoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> ProcessorIds(int id)
        {
            ProcessInfos? ProceSys = GetProcessInfo(id);

            if (ProceSys == null)
            {
                return NotFound(new { message = $"Process {id} not found or inaccessible." });
            }

            return Ok(ProceSys); // Retorna os dados do processo
        }
    }
}
