﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.ML.CLI.Templates.Console
{
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using Microsoft.ML.CLI.Utilities;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class ModelBuilder : ModelBuilderBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            Write(@"//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using ");
            Write(ToStringHelper.ToStringWithCulture(Namespace));
            Write(".Model.DataModels;\r\n");
            Write(ToStringHelper.ToStringWithCulture(GeneratedUsings));
            Write("\r\nnamespace ");
            Write(ToStringHelper.ToStringWithCulture(Namespace));
            Write(".ConsoleApp\r\n{\r\n    public static class ModelBuilder\r\n    {\r\n        private stat" +
                    "ic string TRAIN_DATA_FILEPATH = @\"");
            Write(ToStringHelper.ToStringWithCulture(Path));
            Write("\";\r\n");
if(!string.IsNullOrEmpty(TestPath)){ 
            Write("        private static string TEST_DATA_FILEPATH = @\"");
            Write(ToStringHelper.ToStringWithCulture(TestPath));
            Write("\";\r\n");
 } 
            Write("        private static string MODEL_FILEPATH = @\"../../../../");
            Write(ToStringHelper.ToStringWithCulture(Namespace));
            Write(@".Model/MLModel.zip"";

        // Create MLContext to be shared across the model creation workflow objects 
        // Set a random seed for repeatable/deterministic results across multiple trainings.
        private static MLContext mlContext = new MLContext(seed: 1);

        public static void CreateModel()
        {
            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader : ");
            Write(ToStringHelper.ToStringWithCulture(HasHeader.ToString().ToLowerInvariant()));
            Write(",\r\n                                            separatorChar : \'");
            Write(ToStringHelper.ToStringWithCulture(Regex.Escape(Separator.ToString())));
            Write("\',\r\n                                            allowQuoting : ");
            Write(ToStringHelper.ToStringWithCulture(AllowQuoting.ToString().ToLowerInvariant()));
            Write(",\r\n                                            allowSparse: ");
            Write(ToStringHelper.ToStringWithCulture(AllowSparse.ToString().ToLowerInvariant()));
            Write(");\r\n\r\n");
 if(!string.IsNullOrEmpty(TestPath)){ 
            Write("            IDataView testDataView = mlContext.Data.LoadFromTextFile<ModelInput>(" +
                    "\r\n                                            path: TEST_DATA_FILEPATH,\r\n       " +
                    "                                     hasHeader : ");
            Write(ToStringHelper.ToStringWithCulture(HasHeader.ToString().ToLowerInvariant()));
            Write(",\r\n                                            separatorChar : \'");
            Write(ToStringHelper.ToStringWithCulture(Regex.Escape(Separator.ToString())));
            Write("\',\r\n                                            allowQuoting : ");
            Write(ToStringHelper.ToStringWithCulture(AllowQuoting.ToString().ToLowerInvariant()));
            Write(",\r\n                                            allowSparse: ");
            Write(ToStringHelper.ToStringWithCulture(AllowSparse.ToString().ToLowerInvariant()));
            Write(");\r\n");
}
            Write("            // Build training pipeline\r\n            IEstimator<ITransformer> trai" +
                    "ningPipeline = BuildTrainingPipeline(mlContext);\r\n\r\n");
 if(string.IsNullOrEmpty(TestPath)){ 
            Write("            // Evaluate quality of Model\r\n            Evaluate(mlContext, trainin" +
                    "gDataView, trainingPipeline);\r\n\r\n");
}
            Write("            // Train Model\r\n            ITransformer mlModel = TrainModel(mlConte" +
                    "xt, trainingDataView, trainingPipeline);\r\n");
 if(!string.IsNullOrEmpty(TestPath)){ 
            Write("\r\n            // Evaluate quality of Model\r\n            EvaluateModel(mlContext, " +
                    "mlModel, testDataView);\r\n");
}
            Write("\r\n            // Save model\r\n            SaveModel(mlContext, mlModel, MODEL_FILE" +
                    "PATH, trainingDataView.Schema);\r\n        }\r\n\r\n        public static IEstimator<I" +
                    "Transformer> BuildTrainingPipeline(MLContext mlContext)\r\n        {\r\n");
 if(PreTrainerTransforms.Count >0 ) {
            Write("            // Data process configuration with pipeline data transformations \r\n  " +
                    "          var dataProcessPipeline = ");
 for(int i=0;i<PreTrainerTransforms.Count;i++) 
                                         { 
                                             if(i>0)
                                             { Write("\r\n                                      .Append(");
                                             }
                                             Write("mlContext.Transforms."+PreTrainerTransforms[i]);
                                             if(i>0)
                                             { Write(")");
                                             }
                                         }
                                      if(CacheBeforeTrainer){ 
                                           Write("\r\n                                      .AppendCacheCheckpoint(mlContext)");
                                           } 
            Write(";\r\n");
}
            Write("\r\n            // Set the training algorithm \r\n            var trainer = mlContext" +
                    ".");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".Trainers.");
            Write(ToStringHelper.ToStringWithCulture(Trainer));
 for(int i=0;i<PostTrainerTransforms.Count;i++) 
                                         { 
                                             Write("\r\n                                      .Append(");
                                             Write("mlContext.Transforms."+PostTrainerTransforms[i]);
                                             Write(")");
                                         }
            Write(";\r\n");
 if(PreTrainerTransforms.Count >0 ) {
            Write("            var trainingPipeline = dataProcessPipeline.Append(trainer);\r\n");
 }
else{
            Write("            var trainingPipeline = trainer;\r\n");
}
            Write(@"
            return trainingPipeline;
        }

        public static ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine(""=============== Training  model ==============="");

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine(""=============== End of training process ==============="");
            return model;
        }

");
 if(!string.IsNullOrEmpty(TestPath)){ 
            Write(@"        private static void EvaluateModel(MLContext mlContext, ITransformer mlModel, IDataView testDataView)
        {
            // Evaluate the model and show accuracy stats
            Console.WriteLine(""===== Evaluating Model's accuracy with Test data ====="");
            IDataView predictions = mlModel.Transform(testDataView);
");
if("BinaryClassification".Equals(TaskType)){ 
            Write("            var metrics = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".EvaluateNonCalibrated(predictions, \"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\", \"Score\");\r\n            PrintBinaryClassificationMetrics(metrics);\r\n");
} if("MulticlassClassification".Equals(TaskType)){ 
            Write("            var metrics = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".Evaluate(predictions, \"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\", \"Score\");\r\n            PrintMulticlassClassificationMetrics(metrics);\r\n");
}if("Regression".Equals(TaskType)){ 
            Write("            var metrics = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".Evaluate(predictions, \"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\", \"Score\");\r\n            PrintRegressionMetrics(metrics);\r\n");
} 
            Write("        }\r\n");
}else{
            Write(@"        private static void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            Console.WriteLine(""=============== Cross-validating to get model's accuracy metrics ==============="");
");
if("BinaryClassification".Equals(TaskType)){ 
            Write("            var crossValidationResults = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".CrossValidateNonCalibrated(trainingDataView, trainingPipeline, numberOfFolds: ");
            Write(ToStringHelper.ToStringWithCulture(Kfolds));
            Write(", labelColumnName:\"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\");\r\n            PrintBinaryClassificationFoldsAverageMetrics(crossValidationResu" +
                    "lts);\r\n");
}
if("MulticlassClassification".Equals(TaskType)){ 
            Write("            var crossValidationResults = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: ");
            Write(ToStringHelper.ToStringWithCulture(Kfolds));
            Write(", labelColumnName:\"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\");\r\n            PrintMulticlassClassificationFoldsAverageMetrics(crossValidation" +
                    "Results);\r\n");
}
if("Regression".Equals(TaskType)){ 
            Write("            var crossValidationResults = mlContext.");
            Write(ToStringHelper.ToStringWithCulture(TaskType));
            Write(".CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: ");
            Write(ToStringHelper.ToStringWithCulture(Kfolds));
            Write(", labelColumnName:\"");
            Write(ToStringHelper.ToStringWithCulture(LabelName));
            Write("\");\r\n            PrintRegressionFoldsAverageMetrics(crossValidationResults);\r\n");
}
            Write("        }\r\n");
}
            Write(@"        private static void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($""=============== Saving the model  ==============="");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine(""The model is saved to {0}"", GetAbsolutePath(modelRelativePath));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

");
if("Regression".Equals(TaskType)){ 
            Write("        public static void PrintRegressionMetrics(RegressionMetrics metrics)\r\n   " +
                    "     {\r\n            Console.WriteLine($\"****************************************" +
                    "*********\");\r\n            Console.WriteLine($\"*       Metrics for regression mod" +
                    "el      \");\r\n            Console.WriteLine($\"*----------------------------------" +
                    "--------------\");\r\n            Console.WriteLine($\"*       LossFn:        {metri" +
                    "cs.LossFunction:0.##}\");\r\n            Console.WriteLine($\"*       R2 Score:     " +
                    " {metrics.RSquared:0.##}\");\r\n            Console.WriteLine($\"*       Absolute lo" +
                    "ss: {metrics.MeanAbsoluteError:#.##}\");\r\n            Console.WriteLine($\"*      " +
                    " Squared loss:  {metrics.MeanSquaredError:#.##}\");\r\n            Console.WriteLin" +
                    "e($\"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}\");\r\n            C" +
                    "onsole.WriteLine($\"*************************************************\");\r\n       " +
                    " }\r\n\r\n        public static void PrintRegressionFoldsAverageMetrics(IEnumerable<" +
                    "TrainCatalogBase.CrossValidationResult<RegressionMetrics>> crossValidationResult" +
                    "s)\r\n        {\r\n            var L1 = crossValidationResults.Select(r => r.Metrics" +
                    ".MeanAbsoluteError);\r\n            var L2 = crossValidationResults.Select(r => r." +
                    "Metrics.MeanSquaredError);\r\n            var RMS = crossValidationResults.Select(" +
                    "r => r.Metrics.RootMeanSquaredError);\r\n            var lossFunction = crossValid" +
                    "ationResults.Select(r => r.Metrics.LossFunction);\r\n            var R2 = crossVal" +
                    "idationResults.Select(r => r.Metrics.RSquared);\r\n\r\n            Console.WriteLine" +
                    "($\"*****************************************************************************" +
                    "********************************\");\r\n            Console.WriteLine($\"*       Met" +
                    "rics for Regression model      \");\r\n            Console.WriteLine($\"*-----------" +
                    "--------------------------------------------------------------------------------" +
                    "-----------------\");\r\n            Console.WriteLine($\"*       Average L1 Loss:  " +
                    "     {L1.Average():0.###} \");\r\n            Console.WriteLine($\"*       Average L" +
                    "2 Loss:       {L2.Average():0.###}  \");\r\n            Console.WriteLine($\"*      " +
                    " Average RMS:           {RMS.Average():0.###}  \");\r\n            Console.WriteLin" +
                    "e($\"*       Average Loss Function: {lossFunction.Average():0.###}  \");\r\n        " +
                    "    Console.WriteLine($\"*       Average R-squared:     {R2.Average():0.###}  \");" +
                    "\r\n            Console.WriteLine($\"**********************************************" +
                    "***************************************************************\");\r\n        }\r\n");
 } if("BinaryClassification".Equals(TaskType)){ 
            Write("        public static void PrintBinaryClassificationMetrics(BinaryClassificationM" +
                    "etrics metrics)\r\n        {\r\n            Console.WriteLine($\"********************" +
                    "****************************************\");\r\n            Console.WriteLine($\"*  " +
                    "     Metrics for binary classification model      \");\r\n            Console.Write" +
                    "Line($\"*-----------------------------------------------------------\");\r\n        " +
                    "    Console.WriteLine($\"*       Accuracy: {metrics.Accuracy:P2}\");\r\n            " +
                    "Console.WriteLine($\"*       Auc:      {metrics.AreaUnderRocCurve:P2}\");\r\n       " +
                    "     Console.WriteLine($\"*******************************************************" +
                    "*****\");\r\n        }\r\n\r\n\r\n        public static void PrintBinaryClassificationFol" +
                    "dsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<BinaryClassi" +
                    "ficationMetrics>> crossValResults)\r\n        {\r\n            var metricsInMultiple" +
                    "Folds = crossValResults.Select(r => r.Metrics);\r\n\r\n            var AccuracyValue" +
                    "s = metricsInMultipleFolds.Select(m => m.Accuracy);\r\n            var AccuracyAve" +
                    "rage = AccuracyValues.Average();\r\n            var AccuraciesStdDeviation = Calcu" +
                    "lateStandardDeviation(AccuracyValues);\r\n            var AccuraciesConfidenceInte" +
                    "rval95 = CalculateConfidenceInterval95(AccuracyValues);\r\n\r\n\r\n            Console" +
                    ".WriteLine($\"*******************************************************************" +
                    "******************************************\");\r\n            Console.WriteLine($\"*" +
                    "       Metrics for Binary Classification model      \");\r\n            Console.Wri" +
                    "teLine($\"*----------------------------------------------------------------------" +
                    "--------------------------------------\");\r\n            Console.WriteLine($\"*    " +
                    "   Average Accuracy:    {AccuracyAverage:0.###}  - Standard deviation: ({Accurac" +
                    "iesStdDeviation:#.###})  - Confidence Interval 95%: ({AccuraciesConfidenceInterv" +
                    "al95:#.###})\");\r\n            Console.WriteLine($\"*******************************" +
                    "******************************************************************************\")" +
                    ";\r\n        }\r\n\r\n        public static double CalculateStandardDeviation(IEnumera" +
                    "ble<double> values)\r\n        {\r\n            double average = values.Average();\r\n" +
                    "            double sumOfSquaresOfDifferences = values.Select(val => (val - avera" +
                    "ge) * (val - average)).Sum();\r\n            double standardDeviation = Math.Sqrt(" +
                    "sumOfSquaresOfDifferences / (values.Count() - 1));\r\n            return standardD" +
                    "eviation;\r\n        }\r\n\r\n        public static double CalculateConfidenceInterval" +
                    "95(IEnumerable<double> values)\r\n        {\r\n            double confidenceInterval" +
                    "95 = 1.96 * CalculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1))" +
                    ";\r\n            return confidenceInterval95;\r\n        }\r\n");
} if("MulticlassClassification".Equals(TaskType)){
            Write("        public static void PrintMulticlassClassificationMetrics(MulticlassClassif" +
                    "icationMetrics metrics)\r\n        {\r\n            Console.WriteLine($\"************" +
                    "************************************************\");\r\n            Console.WriteLi" +
                    "ne($\"*    Metrics for multi-class classification model   \");\r\n            Consol" +
                    "e.WriteLine($\"*-----------------------------------------------------------\");\r\n " +
                    "           Console.WriteLine($\"    MacroAccuracy = {metrics.MacroAccuracy:0.####" +
                    "}, a value between 0 and 1, the closer to 1, the better\");\r\n            Console." +
                    "WriteLine($\"    MicroAccuracy = {metrics.MicroAccuracy:0.####}, a value between " +
                    "0 and 1, the closer to 1, the better\");\r\n            Console.WriteLine($\"    Log" +
                    "Loss = {metrics.LogLoss:0.####}, the closer to 0, the better\");\r\n            for" +
                    " (int i = 0; i < metrics.PerClassLogLoss.Count; i++)\r\n            {\r\n           " +
                    "     Console.WriteLine($\"    LogLoss for class {i + 1} = {metrics.PerClassLogLos" +
                    "s[i]:0.####}, the closer to 0, the better\");\r\n            }\r\n            Console" +
                    ".WriteLine($\"************************************************************\");\r\n  " +
                    "      }\r\n\r\n        public static void PrintMulticlassClassificationFoldsAverageM" +
                    "etrics(IEnumerable<TrainCatalogBase.CrossValidationResult<MulticlassClassificati" +
                    "onMetrics>> crossValResults)\r\n        {\r\n            var metricsInMultipleFolds " +
                    "= crossValResults.Select(r => r.Metrics);\r\n\r\n            var microAccuracyValues" +
                    " = metricsInMultipleFolds.Select(m => m.MicroAccuracy);\r\n            var microAc" +
                    "curacyAverage = microAccuracyValues.Average();\r\n            var microAccuraciesS" +
                    "tdDeviation = CalculateStandardDeviation(microAccuracyValues);\r\n            var " +
                    "microAccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(microAccurac" +
                    "yValues);\r\n\r\n            var macroAccuracyValues = metricsInMultipleFolds.Select" +
                    "(m => m.MacroAccuracy);\r\n            var macroAccuracyAverage = macroAccuracyVal" +
                    "ues.Average();\r\n            var macroAccuraciesStdDeviation = CalculateStandardD" +
                    "eviation(macroAccuracyValues);\r\n            var macroAccuraciesConfidenceInterva" +
                    "l95 = CalculateConfidenceInterval95(macroAccuracyValues);\r\n\r\n            var log" +
                    "LossValues = metricsInMultipleFolds.Select(m => m.LogLoss);\r\n            var log" +
                    "LossAverage = logLossValues.Average();\r\n            var logLossStdDeviation = Ca" +
                    "lculateStandardDeviation(logLossValues);\r\n            var logLossConfidenceInter" +
                    "val95 = CalculateConfidenceInterval95(logLossValues);\r\n\r\n            var logLoss" +
                    "ReductionValues = metricsInMultipleFolds.Select(m => m.LogLossReduction);\r\n     " +
                    "       var logLossReductionAverage = logLossReductionValues.Average();\r\n        " +
                    "    var logLossReductionStdDeviation = CalculateStandardDeviation(logLossReducti" +
                    "onValues);\r\n            var logLossReductionConfidenceInterval95 = CalculateConf" +
                    "idenceInterval95(logLossReductionValues);\r\n\r\n            Console.WriteLine($\"***" +
                    "********************************************************************************" +
                    "**************************\");\r\n            Console.WriteLine($\"*       Metrics f" +
                    "or Multi-class Classification model      \");\r\n            Console.WriteLine($\"*-" +
                    "--------------------------------------------------------------------------------" +
                    "---------------------------\");\r\n            Console.WriteLine($\"*       Average " +
                    "MicroAccuracy:    {microAccuracyAverage:0.###}  - Standard deviation: ({microAcc" +
                    "uraciesStdDeviation:#.###})  - Confidence Interval 95%: ({microAccuraciesConfide" +
                    "nceInterval95:#.###})\");\r\n            Console.WriteLine($\"*       Average MacroA" +
                    "ccuracy:    {macroAccuracyAverage:0.###}  - Standard deviation: ({macroAccuracie" +
                    "sStdDeviation:#.###})  - Confidence Interval 95%: ({macroAccuraciesConfidenceInt" +
                    "erval95:#.###})\");\r\n            Console.WriteLine($\"*       Average LogLoss:    " +
                    "      {logLossAverage:#.###}  - Standard deviation: ({logLossStdDeviation:#.###}" +
                    ")  - Confidence Interval 95%: ({logLossConfidenceInterval95:#.###})\");\r\n        " +
                    "    Console.WriteLine($\"*       Average LogLossReduction: {logLossReductionAvera" +
                    "ge:#.###}  - Standard deviation: ({logLossReductionStdDeviation:#.###})  - Confi" +
                    "dence Interval 95%: ({logLossReductionConfidenceInterval95:#.###})\");\r\n         " +
                    "   Console.WriteLine($\"*********************************************************" +
                    "****************************************************\");\r\n\r\n        }\r\n\r\n        " +
                    "public static double CalculateStandardDeviation(IEnumerable<double> values)\r\n   " +
                    "     {\r\n            double average = values.Average();\r\n            double sumOf" +
                    "SquaresOfDifferences = values.Select(val => (val - average) * (val - average)).S" +
                    "um();\r\n            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifference" +
                    "s / (values.Count() - 1));\r\n            return standardDeviation;\r\n        }\r\n\r\n" +
                    "        public static double CalculateConfidenceInterval95(IEnumerable<double> v" +
                    "alues)\r\n        {\r\n            double confidenceInterval95 = 1.96 * CalculateSta" +
                    "ndardDeviation(values) / Math.Sqrt((values.Count() - 1));\r\n            return co" +
                    "nfidenceInterval95;\r\n        }\r\n");
}
            Write("    }\r\n}\r\n");
            return GenerationEnvironment.ToString();
        }

public string Path {get;set;}
public string TestPath {get;set;}
public bool HasHeader {get;set;}
public char Separator {get;set;}
public IList<string> PreTrainerTransforms {get;set;}
public string Trainer {get;set;}
public string TaskType {get;set;}
public string GeneratedUsings {get;set;}
public bool AllowQuoting {get;set;}
public bool AllowSparse {get;set;}
public int Kfolds {get;set;} = 5;
public string Namespace {get;set;}
public string LabelName {get;set;}
public bool CacheBeforeTrainer {get;set;}
public IList<string> PostTrainerTransforms {get;set;}

    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class ModelBuilderBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((generationEnvironmentField == null))
                {
                    generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return generationEnvironmentField;
            }
            set
            {
                generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((errorsField == null))
                {
                    errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((indentLengthsField == null))
                {
                    indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return sessionField;
            }
            set
            {
                sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((GenerationEnvironment.Length == 0) 
                        || endsWithNewline))
            {
                GenerationEnvironment.Append(currentIndentField);
                endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((currentIndentField.Length == 0))
            {
                GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (endsWithNewline)
            {
                GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - currentIndentField.Length));
            }
            else
            {
                GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            Write(textToAppend);
            GenerationEnvironment.AppendLine();
            endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            currentIndentField = (currentIndentField + indent);
            indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((indentLengths.Count > 0))
            {
                int indentLength = indentLengths[(indentLengths.Count - 1)];
                indentLengths.RemoveAt((indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = currentIndentField.Substring((currentIndentField.Length - indentLength));
                    currentIndentField = currentIndentField.Remove((currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            indentLengths.Clear();
            currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}