using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using SpecFlowProjectCepWeb.ActionExtension;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SpecFlowProjectCepWeb.Common
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        private static readonly string PathReport = $"{AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "")}ExtentReportsBuscaCep.html";

        [BeforeTestRun]
        public static void ConfigureReport()
        {
            // Configura o reporter HTML para o ExtentReports
            var reporter = new ExtentHtmlReporter(PathReport);

            // Inicializa uma nova instância do ExtentReports
            _extent = new ExtentReports();

            // Anexa o reporter ao ExtentReports
            _extent.AttachReporter(reporter);
        }

        [BeforeFeature]
        public static void CreateFeature()
        {
            // Cria um novo teste para a feature atual
            _feature = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void CreateScenario()
        {
            // Cria um novo nó de teste para o cenário atual dentro da feature
            _scenario = _feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        public static void InsertReportingSteps()
        {
            // Insere os passos de relatório com base no tipo de definição de passo (Given/Then/When)
            switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _scenario.StepDefinitionGiven();
                    break;

                case StepDefinitionType.Then:
                    _scenario.StepDefinitionThen();
                    break;

                case StepDefinitionType.When:
                    _scenario.StepDefinitionWhen();
                    break;
            }
        }

        [AfterTestRun]
        public static void FlushExtent()
        {
            // Finaliza o ExtentReports e gera o relatório
            _extent.Flush();

            // Abre o relatório no navegador padrão
            System.Diagnostics.Process.Start(PathReport);
        }
    }
}

