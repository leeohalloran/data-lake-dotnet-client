using System.Collections.Generic;
using Microsoft.Azure.Management.DataLake.Analytics;
using MSADLA = Microsoft.Azure.Management.DataLake.Analytics;

namespace AdlClient.Commands
{
    public class LinkedStoreCommands
    {
        internal readonly AdlClient.Models.AnalyticsAccountRef Account;
        internal readonly AnalyticsRestClients RestClients;

        internal LinkedStoreCommands(AdlClient.Models.AnalyticsAccountRef account, AnalyticsRestClients restclients)
        {
            this.Account = account;
            this.RestClients = restclients;
        }

        public void LinkBlobStorageAccount(string storage_account, MSADLA.Models.AddStorageAccountParameters parameters)
        {
            this.RestClients.AccountClient.StorageAccounts.Add(this.Account.ResourceGroup, this.Account.Name, storage_account, parameters);
        }

        public void LinkDataLakeStoreAccount(string storage_account, MSADLA.Models.AddDataLakeStoreParameters parameters)
        {
            this.RestClients.AccountClient.DataLakeStoreAccounts.Add(this.Account.ResourceGroup, this.Account.Name, storage_account, parameters);
        }

        public IEnumerable<MSADLA.Models.DataLakeStoreAccountInfo> ListDataLakeStoreAccounts()
        {
            var pageiter = new Rest.PagedIterator<MSADLA.Models.DataLakeStoreAccountInfo>();
            pageiter.GetFirstPage = () => this.RestClients.AccountClient.DataLakeStoreAccounts.ListByAccount(this.Account.ResourceGroup, this.Account.Name);
            pageiter.GetNextPage = p => this.RestClients.AccountClient.DataLakeStoreAccounts.ListByAccountNext(p.NextPageLink);

            int top = 0;
            var accounts = pageiter.EnumerateItems(top);

            return accounts;
        }

        public IEnumerable<MSADLA.Models.StorageAccountInfo> ListBlobStorageAccounts()
        {
            var pageiter = new Rest.PagedIterator<MSADLA.Models.StorageAccountInfo>();
            pageiter.GetFirstPage = () => this.RestClients.AccountClient.StorageAccounts.ListByAccount(this.Account.ResourceGroup, this.Account.Name);
            pageiter.GetNextPage = p => this.RestClients.AccountClient.StorageAccounts.ListByAccountNext(p.NextPageLink);

            int top = 0;
            var accounts = pageiter.EnumerateItems(top);

            return accounts;
        }

        public IEnumerable<MSADLA.Models.StorageContainer> ListBlobStorageContainers(string storage_account)
        {
            var pageiter = new Rest.PagedIterator<MSADLA.Models.StorageContainer>();
            pageiter.GetFirstPage = () => this.RestClients.AccountClient.StorageAccounts.ListStorageContainers(this.Account.ResourceGroup, this.Account.Name, storage_account);
            pageiter.GetNextPage = p => this.RestClients.AccountClient.StorageAccounts.ListStorageContainersNext(p.NextPageLink);

            int top = 0;
            var items = pageiter.EnumerateItems(top);
            return items;
        }

        public void UnlinkBlobStorageAccount(string storage_account)
        {
            this.RestClients.AccountClient.StorageAccounts.Delete(this.Account.ResourceGroup, this.Account.Name, storage_account);
        }

        public void UnlinkDataLakeStoreAccount(string storage_account)
        {
            this.RestClients.AccountClient.DataLakeStoreAccounts.Delete(this.Account.ResourceGroup, this.Account.Name, storage_account);
        }

        public IEnumerable<MSADLA.Models.SasTokenInfo> ListBlobStorageSasTokens(string storage_account, string container)
        {
            var pageiter = new Rest.PagedIterator<MSADLA.Models.SasTokenInfo>();
            pageiter.GetFirstPage = () => this.RestClients.AccountClient.StorageAccounts.ListSasTokens(this.Account.ResourceGroup, this.Account.Name, storage_account, container);
            pageiter.GetNextPage = p => this.RestClients.AccountClient.StorageAccounts.ListSasTokensNext(p.NextPageLink);

            int top = 0;
            var items = pageiter.EnumerateItems(top);
            return items;
        }
    }
}