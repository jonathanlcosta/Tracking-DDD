using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Enumeradores;
using Tracking.Dominio.Ocorrencias.Servicos.Interfaces;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;

namespace Tracking.Dominio.Ocorrencias.Servicos
{
    public class OcorrenciasServico : IOcorrenciasServico
    {
        private readonly IClientesServico clientesServico;
        private readonly ITransportadorasServico transportadorasServico;
        public OcorrenciasServico(IClientesServico clientesServico, ITransportadorasServico transportadorasServico )
        {
         this.clientesServico = clientesServico;
         this.transportadorasServico = transportadorasServico;   
        }
        public void AdicionarItem(Ocorrencia ocorrencia, OcorrenciaColetaMercadoria? itemOcorrencia)
        {
            throw new NotImplementedException();
        }

        public void AdicionarItem(Ocorrencia ocorrencia, IList<OcorrenciaColetaMercadoria> itensOcorrencia)
        {
            throw new NotImplementedException();
        }

        public Ocorrencia Atualizar(int id, string notaFiscal, int? codCliente, int? codTransportadora,
         DateTime data)
        {
            Cliente cliente = null;
            if (codCliente.HasValue && codCliente != 0)
            {
                cliente = clientesServico.Validar(codCliente.Value);
            }

            Transportadora? transportadora = null;
            if (codTransportadora.HasValue && codTransportadora != 0)
            {
                transportadora = transportadorasServico.ValidarTransportadora(codTransportadora.Value);
            }

            Ocorrencia ocorrencia = Validar(id);

            if (cliente != null && cliente.Id != ocorrencia.Cliente!.Id) ocorrencia.SetCliente(cliente);
            if (transportadora != null && transportadora.CodigoTransportadora != ocorrencia.Transportadora!.CodigoTransportadora) ocorrencia.SetTransportadora(transportadora);
            if (!String.IsNullOrEmpty(notaFiscal)) ocorrencia.SetNotaFiscal(notaFiscal);
            if (data == DateTime.MinValue) ocorrencia.SetData(data);
            return ocorrencia;
        }

        public void Excluir(Ocorrencia ocorrencia)
        {
            throw new NotImplementedException();
        }

        public TipoOcorrenciaEnum GetTipoOcorrencia(Ocorrencia ocorrencia)
        {
            throw new NotImplementedException();
        }

        public Ocorrencia Inserir(Ocorrencia ocorrencia)
        {
            throw new NotImplementedException();
        }

        public Ocorrencia Instanciar(string notaFiscal, Cliente cliente, Transportadora transportadora, IList<OcorrenciaColetaMercadoria> ocorrencias, DateTime data)
        {
            throw new NotImplementedException();
        }

        public Ocorrencia Validar(int codigo)
        {
            throw new NotImplementedException();
        }
    }
}