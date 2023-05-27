using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SpecFlowProjectCepWeb.ActionExtension
{
    public static class ScenarioExtensionMethodHooks
    {
        // Cria um nó de cenário com base no tipo de definição de passo (Given/Then/When)
        private static ExtentTest CreateScenario(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            var scenarioStepContext = ScenarioStepContext.Current.StepInfo.Text;

            switch (stepDefinitionType)
            {
                case StepDefinitionType.Given:
                    return extent.CreateNode<Given>(scenarioStepContext);

                case StepDefinitionType.Then:
                    return extent.CreateNode<Then>(scenarioStepContext);

                case StepDefinitionType.When:
                    return extent.CreateNode<When>(scenarioStepContext);

                default:
                    throw new ArgumentOutOfRangeException(nameof(stepDefinitionType), stepDefinitionType, null);
            }
        }

        // Cria um nó de cenário para falha ou erro com base no tipo de definição de passo (Given/Then/When)
        private static void CreateScenarioFailOrError(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            var error = ScenarioContext.Current.TestError;

            if (error.InnerException == null)
            {
                CreateScenario(extent, stepDefinitionType).Error(error.Message);
            }
            else
            {
                CreateScenario(extent, stepDefinitionType).Fail(error.InnerException);
            }
        }

        // Método de extensão para criar um nó de cenário para uma definição de passo Given
        public static void StepDefinitionGiven(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.Given);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Given);
        }

        // Método de extensão para criar um nó de cenário para uma definição de passo When
        public static void StepDefinitionWhen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.When);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.When);
        }

        // Método de extensão para criar um nó de cenário para uma definição de passo Then
        public static void StepDefinitionThen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.Then);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Then);
        }
    }
}
