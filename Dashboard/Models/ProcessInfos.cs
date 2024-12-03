using System.Diagnostics;

namespace Dashboard.Models
{
    public class ProcessInfos
    {
        public string? Name { get; set; }

        public string? User { get; set; }

        public string? State { get; set; }

        public int? Threads { get; set; }

        public int? UsageCPU { get; set; }

        public int? MemUsage { get; set; }

        // Memória virtual total usada pelo processo./proc/[PID]/status

        public int? VmSize { get; set; }
        //Memória física (Resident Set Size) alocada./proc/[PID]/status

        public int? VmRSS { get; set; }

        // Memória usada para o heap./proc/[PID]/status
        public int? VmData { get; set; }

        //VmStk: Memória usada para a pilha(stack)./proc/[PID]/status
        public int? VmStk { get; set; }

        //Total de páginas: VmRSS / 4 KB
        public int? TotalPages { get; set; }

        public int? PssTotal { get; set; }

        public int? VmExe { get; set; }

        //Size: Tamanho da região de memória.  /proc/[PID]/smaps -> importante

        //VmSize: Memória virtual total usada pelo processo./proc/[PID]/status
        //VmRSS: Memória física (Resident Set Size) alocada./proc/[PID]/status
        //VmData: Memória usada para o heap./proc/[PID]/status
        //VmStk: Memória usada para a pilha(stack)./proc/[PID]/status

        //Contagem de páginas:
        //Cada página de memória tem um tamanho fixo, normalmente 4 KB(em sistemas comuns).
        //Você pode calcular a contagem de páginas com base nos valores de memória fornecidos:
        //Total de páginas: VmRSS / 4 KB
        //Páginas de código: A partir de VmExe.
        //Páginas de heap: A partir de VmData.
        //Páginas de stack: A partir de VmStk.

    }
}
