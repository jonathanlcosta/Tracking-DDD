using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Enumeradores;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Ocorrencias.Servicos.Interfaces
{
    public interface IOcorrenciasServico
    {
        Ocorrencia Validar(int codigo);
        Ocorrencia Instanciar(string notaFiscal, int codCliente, int codTransportadora,
         DateTime data, string observacao);
        Ocorrencia Atualizar(int Id, string notaFiscal, int? codCliente, int? codTransportadora,
         DateTime data, string observacao);
        TipoOcorrenciaEnum GetTipoOcorrencia(Ocorrencia ocorrencia);
        Ocorrencia Inserir(Ocorrencia ocorrencia);
        void AdicionarOcorrencia(Ocorrencia ocorrencia, OcorrenciaColetaMercadoria? itemOcorrencia);
        void AdicionarOcorrencia(Ocorrencia ocorrencia, IList<OcorrenciaColetaMercadoria> itensOcorrencia);
        void Excluir(Ocorrencia ocorrencia);
    }
}