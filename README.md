# FreelanceAnalyzer-Hadoop-C#-Azure-HDInsight
Map Reduce Algorithm for Microsoft HDInsight to analyze stack Exchange comments data for certain user behavior.
It will use hadoop streaming api to execute C# map - reduce algo.
The algo analyses the number of times a user apologizes for misunderstanding the question and replying. The comuation is executed on data dum available at https://archive.org/download/stackexchange/freelancing.stackexchange.com.7z
As per analysis on 0.03% user misunderstands the context and had to correct themselves later.

Steps

1) Install HDInsight Emulator  
2) Install Azure PowerShell
3) Compile application to generate binaries
4) Create directories - on Haddoop command line
hadoop fs -mkdir /FA/
hadoop fs -mkdir /FA/Input
hadoop fs -mkdir /FA/Apps

5) Copy data - on Haddoop command line
hadoop fs -copyFromLocal D:\FSC\*.* \FA\Input

5) Copy Binary - on Haddoop command line
hadoop fs -copyFromLocal C:\EXE\FAMapper.exe /FA/Apps/FAMapper.exe
hadoop fs -copyFromLocal C:\EXE\FAReducer.exe /FA/Apps/FAReducer.exe

To run the MapReduce job by using Azure PowerShell

Set variable on Azure Power Shell
*-*-*-*-*
$clusterName = "http://localhost:50111"
$mrMapper = "FAMapper.exe"
$mrReducer = "FAReducer.exe"
$mrMapperFile = "/FA/Apps/FAMapper.exe"
$mrReducerFile = "/FA/Apps/FAReducer.exe"
$mrInput = "/FA/Input/"
$mrOutput = "/FA/Output"
$mrStatusOutput = "/FA/MRStatusOutput"
*-*-*-*-*

Defile steraming job
*-*-*-*-*
$mrJobDef = New-AzureHDInsightStreamingMapReduceJobDefinition -JobName mrWordCountStreamingJob -StatusFolder $mrStatusOutput -Mapper $mrMapper -Reducer $mrReducer -InputPath $mrInput -OutputPath $mrOutput
$mrJobDef.Files.Add($mrMapperFile)
$mrJobDef.Files.Add($mrReducerFile)
*-*-*-*-*

Create credential Object
*-*-*-*-*
$creds = Get-Credential -Message "Enter password" -UserName "hadoop"
*-*-*-*-*

Submit job
*-*-*-*-*
$mrJob = Start-AzureHDInsightJob -Cluster $clusterName -Credential $creds -JobDefinition $mrJobDef
Wait-AzureHDInsightJob -Credential $creds -job $mrJob -WaitTimeoutInSeconds 3600
*-*-*-*-*

![ScreenShot](https://dl.dropboxusercontent.com/u/686781/Screenshot%20for%20Github/FAH/Jobs.JPG)


![ScreenShot](https://dl.dropboxusercontent.com/u/686781/Screenshot%20for%20Github/FAH/result.JPG)

Find result at >
hadoop fs -ls /FA/Output/
hadoop fs -cat /FA/Output/part-00000
