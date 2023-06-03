using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SharpCompress.Common;


namespace SpecFlowProjectCepWeb.PageObjects
{
    public class Page
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private IWebElement _inputSearchBox;


        public Page(IWebDriver webDriver)
        {
            _driver = webDriver;
        }

        public void visit(string endpoint)
        {
            Thread.Sleep(3000);

            _driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/");
            _driver.Manage().Window.Maximize();

        }

        public void SearchBy(string inserirCep)
        {
            // Criação de uma instância do WebDriverWait para esperas explícitas
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Aguarda até que o elemento de entrada de texto seja visível na página
            var inputSearchBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("endereco")));

            // Insere o valor do CEP no elemento de entrada de texto
            inputSearchBox.SendKeys(inserirCep);

            // Localiza o botão de pesquisa por ID e o clica
            var searchButton = _driver.FindElement(By.Id("btn_pesquisar"));
            searchButton.Click();
        }
        // Verifica se a mensagem de CEP não encontrado está sendo exibida
        public bool IsCepNotFoundMessageDisplayed()
        {
            // Criação de uma instância do WebDriverWait para esperas explícitas
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            var cepNotFoundMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mensagem-resultado-alerta")));
            return cepNotFoundMessage.Displayed;
        }

        // Volta para a página inicial
        public void GoBackToHomePage()
        {
            var clickBtn = _driver.FindElement(By.Id("btn_nbusca"));
            clickBtn.Click();

        }

        public bool IsvalidarResultadoCepCorreto(string resultado)
        {

            // Criação de uma instância do WebDriverWait para esperas explícitas
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Aguarda até que o elemento de entrada de texto seja visível na página
            var resultadoElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("resultado")));

            return resultadoElement.Displayed;
        }

        public void visitRastreio(string endpointRastreio)
        {
            Thread.Sleep(3000);
            _driver.Navigate().GoToUrl("https://chat.correios.com.br/");
            _driver.Manage().Window.Maximize();
        }

        public void SearchRastreamentoBy(string inserirCodRastreamento)
        {
            // Criação de uma instância do WebDriverWait para esperas explícitas
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.FindElement(By.Id("celular")).SendKeys("11111111111");
            // Aguarda até que o elemento de entrada de texto seja visível na página
            var inputSearchBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("objeto")));

            // Insere o valor do CEP no elemento de entrada de texto
            inputSearchBox.SendKeys(inserirCodRastreamento);

            
        }
        public bool IsvalidarResultadoCodigo(string resultado)
        {

            // Criação de uma instância do WebDriverWait para esperas explícitas
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            var clickBtn = _driver.FindElement(By.Id("entrar"));
            clickBtn.Click();
            Thread.Sleep(3000);
            // Aguarda até que o elemento de entrada de texto seja visível na página
            var resultadoElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("alerta")));

            return resultadoElement.Displayed;
        }
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
