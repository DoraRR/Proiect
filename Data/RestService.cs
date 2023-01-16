using GymManagerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using System.Net.Http.Headers;

namespace GymManagerApp.Data
{
    internal class RestService : IRestService
    {
        HttpClient client;
        string RestUrl = "https://localhost:7137/api/";

        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlanListsAsync()
        {
            List<WorkoutPlan> items = new List<WorkoutPlan>();
            Uri uri = new Uri(RestUrl + "WorkoutPlans");
            try
            {
                
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<WorkoutPlan>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return items;

        }

        public async Task<List<Workout>> GetWorkoutListsAsync()
        {
            List<Workout> items = new List<Workout>();
            Uri uri = new Uri(RestUrl + "Workouts");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Workout>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return items;

        }

        public async Task<bool> SaveWorkoutPlanAsync(WorkoutPlanCrud item, bool isNewItem)
        {
            Uri uri = new Uri(RestUrl + "WorkoutPlans");
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri + "/" + item.Id, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem successfully saved.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<bool> SaveWorkoutAsync(Workout item, bool isNewItem)
        {
            Uri uri = new Uri(RestUrl + "Workouts");
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri + "/" + item.Id, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    return true;
                    Console.WriteLine(@"\tTodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteWorkoutPlanAsync(int id)
        {
            Uri uri = new Uri(RestUrl + "WorkoutPlans/" + id);
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            Uri uri = new Uri(RestUrl + "Workouts/" + id);
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<string> LoginAsync(LoginData login)
        {

            Uri uri = new Uri(RestUrl + "Authenticate/login");


            try
            {
                string json = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uri, content).Result; ;
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TokenData token = JsonConvert.DeserializeObject<TokenData>(responseContent);
                    return token.value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                var toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await toast.Show();
            }
            return null;
        }

        public void SetToken(string token)
        {
            client.DefaultRequestHeaders.Add("Api-Key", token);
        }
    }
}
