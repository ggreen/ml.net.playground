
```shell
dotnet tool install -g mlnet-osx-arm64
```

```shell

 dotnet sln add car-maintenance/car-maintenance.csproj

```


```shell
wget https://archive.ics.uci.edu/ml/machine-learning-databases/00331/sentiment%20labelled%20sentences.zip
```

```shell
cd sentiment\ labelled\ sentences
mlnet classification --dataset "yelp_labelled.txt" --label-col 1 --has-header false --name SentimentModel  --train-time 60
```

Output

```
Start Training
start multiclass classification
Evaluate Metric: MacroAccuracy                                                                                            
Available Trainers: LGBM,FASTFOREST,FASTTREE,LBFGS,SDCA                                                                   
Training time in second: 100                                                                                              
|      Trainer                             MacroAccuracy Duration    |                                                    
|--------------------------------------------------------------------|                                                    
|0     FastTreeOva                         0.7096     0.4810         |                                                    
|1     FastTreeOva                         0.7571     0.1880         |                                                    
|2     FastTreeOva                         0.7463     0.1930         |                                                    
|3     FastTreeOva                         0.7452     0.3070         |                                                    
|4     FastTreeOva                         0.7696     0.2290         |                                                    
|5     FastTreeOva                         0.7212     0.1710         |                                                    
|7     SdcaMaximumEntropyMulti             0.5000     0.1180         |                                                    
|8     FastForestOva                       0.7244     0.3810         |                                                    
|9     SdcaMaximumEntropyMulti             0.5000     0.0800         |                                                    
|10    FastTreeOva                         0.8150     2.9770         |                                                    
|11    LbfgsMaximumEntropyMulti            0.7796     0.1370         |                                                    
|12    LbfgsMaximumEntropyMulti            0.6163     0.0590         |                                                    
|13    SdcaLogisticRegressionOva           0.5000     0.1860         |                                                    
|14    FastForestOva                       0.7014     0.3490         |                                                    
|15    LbfgsMaximumEntropyMulti            0.7569     0.0680         |                                                    
|16    FastTreeOva                         0.7011     2.0130         |                                                    
|17    LbfgsMaximumEntropyMulti            0.7918     0.0690         |                                                    
|18    LbfgsMaximumEntropyMulti            0.6443     0.0480         |                                                    
|19    LbfgsMaximumEntropyMulti            0.8724     0.1190         |                                                    
|20    LbfgsMaximumEntropyMulti            0.7693     0.0500         |                                                    
|21    LbfgsMaximumEntropyMulti            0.8496     0.2830         |                                                    
|22    LbfgsMaximumEntropyMulti            0.8494     0.3770         |                                                    
|23    LbfgsMaximumEntropyMulti            0.7693     0.0550         |                                                    
|24    LbfgsMaximumEntropyMulti            0.7122     0.0630         |                                                    
|25    LbfgsMaximumEntropyMulti            0.8613     0.2410         |                                                    
|26    LbfgsMaximumEntropyMulti            0.5000     0.0290         |                                                    
|27    SdcaMaximumEntropyMulti             0.7230     0.0770         |                                                    
|28    FastForestOva                       0.7920     0.4700         |                                                    
|29    FastForestOva                       0.7355     0.3420         |                                                    
|31    FastForestOva                       0.7807     4.3400         |                                                    
|32    FastTreeOva                         0.7693     2.3300         |                                                    
|33    LbfgsMaximumEntropyMulti            0.8613     0.2430         |                                                    
|34    LbfgsMaximumEntropyMulti            0.8613     0.2440         |                                                    
|35    LbfgsMaximumEntropyMulti            0.7579     0.0490         |                                                    
|36    LbfgsMaximumEntropyMulti            0.7460     0.0480         |                                                    
|37    FastForestOva                       0.7577     0.6640         |                                                    
|38    LbfgsMaximumEntropyMulti            0.8031     0.3860         |                                                    
|39    LbfgsLogisticRegressionOva          0.7452     0.1200         |                                                    
|40    LbfgsMaximumEntropyMulti            0.6660     0.0760         |                                                    
|41    LbfgsLogisticRegressionOva          0.8377     0.3100         |                                                    
|42    LbfgsLogisticRegressionOva          0.6646     0.0850         |                                                    
|43    LbfgsLogisticRegressionOva          0.8613     0.3020         |                                                    
|45    LbfgsLogisticRegressionOva          0.8613     0.3030         |                                                    
|46    FastForestOva                       0.7357     0.3200         |                                                    
|47    LbfgsLogisticRegressionOva          0.5000     0.0530         |                                                    
|48    LbfgsMaximumEntropyMulti            0.8613     0.2420         |                                                    
|49    LbfgsMaximumEntropyMulti            0.8491     0.4700         |                                                    
|50    LbfgsMaximumEntropyMulti            0.8261     0.0570         |                                                    
|51    LbfgsMaximumEntropyMulti            0.8613     0.2000         |                                                    
|52    SdcaMaximumEntropyMulti             0.7223     0.0770         |                                                    
|53    LbfgsMaximumEntropyMulti            0.8375     0.0930         |                                                    
|54    LbfgsMaximumEntropyMulti            0.8491     0.0930         |                                                    
|55    LbfgsMaximumEntropyMulti            0.8496     0.1260         |                                                    
|56    LbfgsMaximumEntropyMulti            0.8607     0.0810         |                                                    
|57    LbfgsMaximumEntropyMulti            0.8613     0.1900         |                                                    
|58    LbfgsMaximumEntropyMulti            0.8496     0.1320         |                                                    
|59    LbfgsMaximumEntropyMulti            0.8724     0.0940         |                                                    
|60    FastForestOva                       0.7690     0.8620         |                                                    
|61    LbfgsMaximumEntropyMulti            0.8724     0.1260         |                                                    
|62    LbfgsMaximumEntropyMulti            0.8610     0.1080         |                                                    
|63    LbfgsMaximumEntropyMulti            0.8610     0.1260         |                                                    
|64    LbfgsMaximumEntropyMulti            0.8724     0.1300         |                                                    
|65    LbfgsMaximumEntropyMulti            0.8724     0.1160         |                                                    
|66    LbfgsMaximumEntropyMulti            0.8610     0.1220         |                                                    
|67    SdcaLogisticRegressionOva           0.5000     0.1940         |                                                    
|68    LbfgsMaximumEntropyMulti            0.8724     0.1340         |                                                    
|69    LbfgsMaximumEntropyMulti            0.8724     0.1070         |                                                    
|70    LbfgsMaximumEntropyMulti            0.8724     0.1030         |                                                    
|71    LbfgsMaximumEntropyMulti            0.8724     0.1130         |                                                    
|72    SdcaMaximumEntropyMulti             0.5000     0.0790         |                                                    
|73    LbfgsMaximumEntropyMulti            0.8724     0.1440         |                                                    
[Source=AutoMLExperiment, Kind=Info] cancel training because cancellation token is invoked...                             
|--------------------------------------------------------------------|                                                    
|                          Experiment Results                        |
|--------------------------------------------------------------------|
|                               Summary                              |
|--------------------------------------------------------------------|
|ML Task: multiclass classification                                  |
|Dataset: /Users/Projects/solutions/dataScience/playground/ml.net/sentiment labelled sentences/yelp_labelled.txt|
|Label : col1                                                        |
|Total experiment time :    99.0000 Secs                             |
|Total number of models explored: 75                                 |
|--------------------------------------------------------------------|
|                        Top 5 models explored                       |
|--------------------------------------------------------------------|
|      Trainer                             MacroAccuracy Duration    |
|--------------------------------------------------------------------|
|19    LbfgsMaximumEntropyMulti            0.8724     0.1190         |
|59    LbfgsMaximumEntropyMulti            0.8724     0.0940         |
|61    LbfgsMaximumEntropyMulti            0.8724     0.1260         |
|64    LbfgsMaximumEntropyMulti            0.8724     0.1300         |
|65    LbfgsMaximumEntropyMulti            0.8724     0.1160         |
|--------------------------------------------------------------------|
[Source=AutoMLExperiment, Kind=Info] cancel training because cancellation token is invoked...
save SentimentModel.mbconfig to /Users/Projects/solutions/dataScience/playground/ml.net/sentiment labelled sentences/SentimentModel
Generating a console project for the best pipeline at location : /Users/Projects/solutions/dataScience/playground/ml.net/sentiment labelled sentences/SentimentModel
```

