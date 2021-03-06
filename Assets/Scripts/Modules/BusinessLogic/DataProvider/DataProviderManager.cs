﻿using Model.Item;
using Modules.BusinessLogic.Core;
using Modules.BusinessLogic.Session;
using RestSharp;
using UnityEngine;

namespace Modules.BusinessLogic.DataProvider
{
    public class DataProviderManager : Manager
    {
        private const string StatusURL = " https://dev3r02.elysium.today/inventory/status";
        private const string Auth = "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";

        public override void Inject(SessionManager session)
        {
            session.InventoryManager.Added += item => ChangeItemStatus(item.ItemData.ID, ItemState.Add);
            session.InventoryManager.Selected += item => ChangeItemStatus(item.ItemData.ID, ItemState.Select);
        }

        /// <summary>
        /// Send post request to server with changed item state
        /// </summary>
        /// <param name="id">Item unique id</param>
        /// <param name="state">Updated item state</param>
        private void ChangeItemStatus(int id, ItemState state)
        {
            var client = new RestClient(StatusURL) {Timeout = -1};
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Auth", Auth);
            request.AddParameter("ID", id);
            request.AddParameter("State", state);
            
            var response = client.ExecutePostTaskAsync(request).Result;
            Debug.Log($"Response. {response.IsSuccessful}: {response.Content}");
        }
    }
}
