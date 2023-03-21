using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Enumeradores;
using Tracking.Dominio.Ocorrencias.Repositorios;
using Tracking.Dominio.Ocorrencias.Servicos.Interfaces;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;

namespace Tracking.Dominio.Ocorrencias.Servicos
{
    public class OcorrenciasServico : IOcorrenciasServico
    {
        private readonly IClientesServico clientesServico;
        private readonly ITransportadorasServico transportadorasServico;
        private readonly IOcorrenciasRepositorio ocorrenciasRepositorio;
        public OcorrenciasServico(IClientesServico clientesServico, ITransportadorasServico transportadorasServico,
       IOcorrenciasRepositorio ocorrenciasRepositorio )
        {
         this.clientesServico = clientesServico;
         this.ocorrenciasRepositorio = ocorrenciasRepositorio;
         this.transportadorasServico = transportadorasServico;   
        }
        public void AdicionarOcorrencia(Ocorrencia ocorrencia, OcorrenciaColetaMercadoria? itemOcorrencia)
        {
           if (itemOcorrencia == null)
                throw new ArgumentNullException("O item da ocorrência não pode ser nulo.");
            ocorrencia.Ocorrencias!.Add(itemOcorrencia);
        }

        public void AdicionarOcorrencia(Ocorrencia ocorrencia, IList<OcorrenciaColetaMercadoria> itensOcorrencia)
        {
           if(itensOcorrencia != null)
            {
                foreach(var item in itensOcorrencia)
                {
                    ocorrencia.Ocorrencias!.Add(item!);
                }
            }
        }

        public Ocorrencia Atualizar(int id, string notaFiscal, int? codCliente, int? codTransportadora,
         DateTime data, string observacao)
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
            ocorrencia.SetObservacao(observacao);
            return ocorrencia;
        }

        public void Excluir(Ocorrencia ocorrencia)
        {
            ocorrenciasRepositorio.Excluir(ocorrencia);
        }

        public TipoOcorrenciaEnum GetTipoOcorrencia(Ocorrencia ocorrencia)
        {
            return ocorrencia.Tipo;
        }

        public Ocorrencia Inserir(Ocorrencia ocorrencia)
        {
            return ocorrenciasRepositorio.Inserir(ocorrencia);
        }

        public Ocorrencia Instanciar(string notaFiscal, int codCliente, int codTransportadora,
         DateTime data, string observacao)
        {
           Cliente cliente = clientesServico.Validar(codCliente);
            Transportadora transportadora = transportadorasServico.ValidarTransportadora(codTransportadora);
            return new Ocorrencia(notaFiscal, cliente, transportadora,
            new List<OcorrenciaColetaMercadoria>(), data, observacao);
        }

        public Ocorrencia Validar(int codigo)
        {
            if (codigo == 0)
                throw new ArgumentException("Insira um código de ocorrência valido.");

           Ocorrencia ocorrencia = ocorrenciasRepositorio.Recuperar(codigo);

            if (ocorrencia == null)
                throw new ArgumentNullException("Ocorrência não encontrada.");
            return ocorrencia;
        }
    }
}