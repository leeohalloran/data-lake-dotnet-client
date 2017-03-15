﻿namespace AzureDataLakeClient.Analytics
{
    public class AnalyticsAccount
    {
        public string Name;
        public AzureDataLakeClient.Rm.Subscription Subscription;
        public AzureDataLakeClient.Rm.ResourceGroup ResourceGroup;

        public AnalyticsAccount(string name, AzureDataLakeClient.Rm.Subscription sub, AzureDataLakeClient.Rm.ResourceGroup rg)
        {
            this.Name = name;
            this.Subscription = sub;
            this.ResourceGroup = rg;
        }
    }
}