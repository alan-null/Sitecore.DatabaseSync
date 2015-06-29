namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    interface IDataSyncPipelineProcessor
    {
        void Process(DataSyncPipelineArgs args);
    }
}