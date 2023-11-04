using System;
using System.IO;
using Imani.Solutions.Core.API.Util;

// using HeartDiseasePredictionConsoleApp.DataStructures;
using Microsoft.ML;
using Microsoft.ML.Data;
using PlayGround.ML.NET.Predictive.Maintenance.Domain;

namespace PlayGround.ML.NET.Predictive.Maintenance
{
    public class Program
    {
        private static string basePath = ".";
        private static string trainDataRelativePath = $"{basePath}/resources/ml-data/CarData.csv";
        private static string testDataRelativePath = $"{basePath}/resources/ml-data/CarTest.csv";

        private static string trainDataPath = trainDataRelativePath;
        private static string testDataPath = testDataRelativePath;

        private static string modelsRelativePath = "runtime";
        private static string ModelPath = $"{modelsRelativePath}/CarMaintenance.zip";


        public static void Main(string[] args)
        {
            var mlContext = new MLContext();
            train(mlContext);

            testPrediction(mlContext);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        private static void train(MLContext mlContext)
        {
            // STEP 1: Common data loading configuration
            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<CarMaintenance>();

            // DatabaseLoader.Column[] columns = null;
            //var trainingDataView = mlContext.Data.LoadFromTextFile<CarMaintenance>(trainDataPath, hasHeader: true, separatorChar: ',');
            var trainingDataView = TrainFromDb(mlContext);
            
            var testDataView = mlContext.Data.LoadFromTextFile<CarMaintenance>(testDataPath, hasHeader: true, separatorChar: ',');

            string outColumn = "Features";
            // STEP 2: Concatenate the features and set the training algorithm
            var pipeline = mlContext.Transforms.Concatenate(
                outColumn,
                "vehicle_type",
                "brand",
                "model",
                "engine_type",
                "make_year",
                "region",
                "mileage_range",
                "mileage",
                "oil_filter",
                "engine_oil",
                "washer_plug_drain",
                "dust_and_pollen_filter",
                "whell_alignment_and_balancing",
                "air_clean_filter",
                "fuel_filter",
                "spark_plug",
                "brake_fluid",
                "brake_and_clutch_oil",
                "transmission_fluid",
                "brake_pads",
                "clutch",
                "coolant",
                "cost"
                )
                .Append(mlContext.BinaryClassification.Trainers.FastTree(
                    labelColumnName: "label", featureColumnName: outColumn));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer trainedModel = pipeline.Fit(trainingDataView);
            Console.WriteLine("=============== Finish the train model. Push Enter ===============");

            Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");
            var predictions = trainedModel.Transform(testDataView);

            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "label", scoreColumnName: "Score");
            Console.WriteLine($"************************************************************");
            Console.WriteLine($"*       Metrics for {trainedModel.ToString()} binary classification model      ");
            Console.WriteLine($"*-----------------------------------------------------------");
            Console.WriteLine($"*       Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"*       Area Under Roc Curve:      {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"*       Area Under PrecisionRecall Curve:  {metrics.AreaUnderPrecisionRecallCurve:P2}");
            Console.WriteLine($"*       F1Score:  {metrics.F1Score:P2}");
            Console.WriteLine($"*       LogLoss:  {metrics.LogLoss:#.##}");
            Console.WriteLine($"*       LogLossReduction:  {metrics.LogLossReduction:#.##}");
            Console.WriteLine($"*       PositivePrecision:  {metrics.PositivePrecision:#.##}");
            Console.WriteLine($"*       PositiveRecall:  {metrics.PositiveRecall:#.##}");
            Console.WriteLine($"*       NegativePrecision:  {metrics.NegativePrecision:#.##}");
            Console.WriteLine($"*       NegativeRecall:  {metrics.NegativeRecall:P2}");
            Console.WriteLine($"************************************************************");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);
            Console.WriteLine("=============== Model Saved ============= ");

            testPrediction(mlContext);
        }

        private static IDataView TrainFromDb(MLContext mlContext)
        {
            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<CarMaintenance>();
           
            var connectionString = new ConfigSettings().GetProperty("ConnectionString");


            string sqlCommand = "SELECT slno,vehicle_type,brand,model,engine_type,make_year,region,mileage_range,mileage,oil_filter,engine_oil,washer_plug_drain,dust_and_pollen_filter,whell_alignment_and_balancing,air_clean_filter,fuel_filter,spark_plug,brake_fluid,brake_and_clutch_oil,transmission_fluid,brake_pads,clutch,coolant,cost,(CASE WHEN label=1 THEN true ELSE false END)::boolean as label FROM cars.maintenance_training";

            DatabaseSource dbSource = new DatabaseSource(Npgsql.NpgsqlFactory.Instance, connectionString, sqlCommand);

            return loader.Load(dbSource);
        }

        private static void testPrediction(MLContext mlContext)
        {
            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predictionEngine = mlContext.Model.CreatePredictionEngine<CarMaintenance, MaintenancePrediction>(trainedModel);

            CarMaintenance carMaintenance = new CarMaintenance();
            carMaintenance.mileage = 100000;
            carMaintenance.brand = 1;
            carMaintenance.mileage_range = 5000;
            carMaintenance.make_year = 1980;

            var prediction = predictionEngine.Predict(carMaintenance);

            Console.WriteLine($"=============== Single Prediction  ===============");
                Console.WriteLine($"Prediction Value: {prediction.Prediction} ");
                Console.WriteLine($"Prediction: {(prediction.Prediction ? "Maintenance needed!!!" : "Maintenance not needed" )} ");
                Console.WriteLine($"Probability: {prediction.Probability} ");
                Console.WriteLine($"==================================================");

            // foreach (var CarMaintenance in HeartSampleData.CarMaintenanceList)
            // {
                // var prediction = predictionEngine.Predict(CarMaintenance);

                // Console.WriteLine($"=============== Single Prediction  ===============");
                // Console.WriteLine($"Age: {CarMaintenance.Age} ");
                // Console.WriteLine($"Sex: {CarMaintenance.Sex} ");
                // Console.WriteLine($"Cp: {CarMaintenance.Cp} ");
                // Console.WriteLine($"TrestBps: {CarMaintenance.TrestBps} ");
                // Console.WriteLine($"Chol: {CarMaintenance.Chol} ");
                // Console.WriteLine($"Fbs: {CarMaintenance.Fbs} ");
                // Console.WriteLine($"RestEcg: {CarMaintenance.RestEcg} ");
                // Console.WriteLine($"Thalac: {CarMaintenance.Thalac} ");
                // Console.WriteLine($"Exang: {CarMaintenance.Exang} ");
                // Console.WriteLine($"OldPeak: {CarMaintenance.OldPeak} ");
                // Console.WriteLine($"Slope: {CarMaintenance.Slope} ");
                // Console.WriteLine($"Ca: {CarMaintenance.Ca} ");
                // Console.WriteLine($"Thal: {CarMaintenance.Thal} ");
                // Console.WriteLine($"Prediction Value: {prediction.Prediction} ");
                // Console.WriteLine($"Prediction: {(prediction.Prediction ? "A disease could be present" : "Not present disease" )} ");
                // Console.WriteLine($"Probability: {prediction.Probability} ");
                // Console.WriteLine($"==================================================");
                // Console.WriteLine("");
                // Console.WriteLine("");
            // }

        }


        public static string absolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;

        }
    }
}