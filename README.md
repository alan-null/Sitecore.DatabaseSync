---
# This project has been discontinued.
Try [Unicorn](https://github.com/kamsar/Unicorn) by @kamsar instead.

---

# Sitecore.DatabaseSync
Sitecore module for database synchronization. 


## Why?
Updating Sitecore content manually is really painful when you have to do it very often.
That is why I've decided to create tool which will do it automatically. 

Forget about times when you had to dig deep inside content tree to update/revert single item.


## How it works?
Items will be updated/reverted each time you reload an application or execute the following piece of code `DatabaseSynchronizer.SynchronizeDatabase();`

List of items that should be updated/reverted are delivered via *changes provider*. 

## How use it?
At the moment module provides only basic *changes provider* used to update items. It is called `BasicProvider` and it returns list of files that are located in `Data/serialization` folder.

Current provider is just an example and doesn't contains any smart logic, so all items from the serialization folder will be updated/reverted when synchronization will take place.

You can easily implement your own provider.

#### Steps to implement own provider
**1**  Create new class library project

**2**  Crete new class and implement `IDatabaseSyncChangesProvider` interface
All you have to do is to provide list of items that were changed since last time. Returned objects type below:
```csharp
    public class ItemChange
    {
        public string Database { get; set; }
        public string SitecorePath { get; set; }
    }
```
*Example values*
```
string SitecorePath = "/sitecore/content/Home";
string Database = "master";
```
**3**  Override existing configuration with your provider
```xml
  <sitecore>
    <databaseSync>
      <patch:delete />
    </databaseSync>
    <databaseSync>
      <changesProvider type="Sitecore.DatabaseSync.Providers.Custom, Sitecore.DatabaseSync.CustomProviders"/>
    </databaseSync>
  </sitecore>
</configuration>
 ```


#### How to override/extend module features
*Standard Sitecore rules to extend/modify existing code*

There is a pipeline called *SynchronizeDatabase*. 
Implement your own processor using `IDataSyncPipelineProcessor` interface and override existing processor or just add it to extend existing pipeline.