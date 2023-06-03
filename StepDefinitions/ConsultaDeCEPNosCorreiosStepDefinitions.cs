using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProjectCepWeb.Common;
using SpecFlowProjectCepWeb.PageObjects;
using TechTalk.SpecFlow;

namespace SpecFlowProjectCepWeb.StepDefinitions
{
    [Binding]
    public  sealed class ConsultaDeCEPNosCorreiosStepDefinitions : ObjectContainerCommon
    {
        private readonly Page _CepPage;
        private string _cep;
        private readonly IWebDriver _driver;
        string endpointCep = "app/endereco/index.php";

        string endpointRastreio = "app/index.php";
        public ConsultaDeCEPNosCorreiosStepDefinitions(IObjectContainer objectContainer) : base(objectContainer)
        {
            _CepPage = new Page(this.Driver); // aqui o Driver que já foi criado
        }

        [Given(@"que estou no site dos Correios")]
        public void GivenQueEstouNoSiteDosCorreios()
        {
            _CepPage.visit(endpointCep);

        }

        [When(@"eu procuro pelo CEP que não existe '(.*)'")]
        public void WhenEuProcuroPeloCEP(string cep)
        {
            // Armazena o CEP para uso posterior
            _cep = cep;
            // Realiza a pesquisa do CEP na página
            _CepPage.SearchBy(cep);
        }

        [Then(@"o CEP pesquisado não deve existir")]
        public void ThenOCEPNaoDeveExistir()
        {
            Assert.IsTrue(_CepPage.IsCepNotFoundMessageDisplayed());
            
        }

        [Then(@"devo voltar para a tela inicial")]
        public void ThenEuVoltoParaATelaInicial()
        {
            _CepPage.GoBackToHomePage();
        }
        [When(@"eu procuro pelo CEP '(.*)'")]
        public void WhenEuProcuroPeloCEPCerto(string cep)
        {
            // Armazena o CEP para uso posterior
            _cep = cep;
            // Realiza a pesquisa do CEP na página
            _CepPage.SearchBy(cep);
        }

        [Then(@"o resultado deve ser '(.*)'")]
        public void ThenOResultadoDeveSer(string resultado)
        {
            Assert.IsTrue(_CepPage.IsvalidarResultadoCepCorreto(resultado));
        }

        [Then(@"eu volto para a tela inicial")]
        public void EuVoltoParaATelaInicial()
        {
            _CepPage.GoBackToHomePage();
        }
        
        [Given(@"que estou no site dos Correios de Rastreio")]
        public void GivenQueEstouNoSiteDosCorreiosDeRastreio()
        {
            _CepPage.visitRastreio(endpointRastreio);

        }

        [When(@"eu procuro pelo código de rastreamento '(.*)'")]
        public void WhenEuProcuroPeloCodigoDeRastreamento(string rastreioText )
        {
             
            _CepPage.SearchRastreamentoBy(rastreioText);
            
        }

       [Then(@"o código não está correto '(.*)'")]
        public void ThenOCodigoNaoEstaCorreto(string resultadoCodigo)
        {
            Assert.IsTrue(_CepPage.IsvalidarResultadoCodigo(resultadoCodigo));
        }

        [Then(@"eu fecho o navegador")]
        public void ThenEuFechoONavegador()
        {
            _CepPage.CloseBrowser();
        }
    }
}
