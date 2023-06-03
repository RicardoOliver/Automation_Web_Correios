using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace SpecFlowProjectCepWeb.Common
{
    public class ObjectContainerCommon : IDisposable
    {
        // Propriedade para armazenar a instância do WebDriver
        protected IWebDriver Driver { get; }

        // Construtor que recebe um objeto de container de objetos (IObjectContainer)
        public ObjectContainerCommon(IObjectContainer objectContainer)
        {
            // Registra uma instância do ChromeDriver no container de objetos
            objectContainer.RegisterInstanceAs(new FirefoxDriver(), typeof(IWebDriver));

            // Resolve (obtém) a instância do WebDriver a partir do container de objetos
            Driver = objectContainer.Resolve<IWebDriver>();
        }

        // Implementação do método Dispose da interface IDisposable
        public void Dispose()
        {
            // Encerra o WebDriver e libera os recursos associados
            Driver?.Quit();
            Driver?.Dispose();

            // Solicita a anulação para otimização de desempenho
            GC.SuppressFinalize(this);
        }
    }
}
