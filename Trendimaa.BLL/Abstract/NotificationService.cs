using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using AutoMapper;
using Notification = Trendeimaa.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Trendimaa.DTO;
using Trendimaa.Common;

namespace Trendimaa.BLL.Abstract
{
    public class NotificationService : Service<Trendeimaa.Entities.Notification>, INotificationService
    {
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;
        public readonly IValidator<Notification> _validator;
        public readonly IUOW _uow;

        public NotificationService(IValidator<Trendeimaa.Entities.Notification> validator, IUOW uow,IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
            
        }

        public async Task<IResponse<List<NotificationDTO>>> GetSellerNotification(int sellerId)
        {
           var data=await _context.Notifications.Where(i=>i.SellerId==sellerId).ToListAsync();
           var mapped= _mapper.Map<List<NotificationDTO>>(data);
          return new Response<List<NotificationDTO>>(ResponseType.Success,mapped);
        }

        public async Task<IResponse<List<NotificationDTO>>> GetUserNotification(int appuserId)
        {
            var data = await _context.Notifications.Where(i => i.SellerId == appuserId).ToListAsync();
            var mapped = _mapper.Map<List<NotificationDTO>>(data);
            return new Response<List<NotificationDTO>>(ResponseType.Success, mapped);
        }

        public async Task<BatchResponse> SendNotification(List<string> registrationTokens, string title, string body)
        {
            var app = FirebaseApp.DefaultInstance;
            if (FirebaseApp.DefaultInstance == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(
                        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"trendimaa-57b1c\",\r\n  \"private_key_id\": \"5338722526999e67bba4826deb49cd33859db66f\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC1QlaTHQPy4oYN\\nanlxJYayKrSRvMyL+llNtIEu5CVMlEPymA8n73RoibeBsA+TMpw9jLnV5BKy7yJt\\ny3kdU0iv44r6Yh6hPvtzSbTtWZtg9ieFQIbRzKBnrn52wYaTlmy07QLcP3ymqu3B\\ncQqBp9PrR8qaOcPrNpiKXh3u+hK+lXdOf0SEgh9H0YS7y5IwmHbJv4iyfRRVLpPv\\nzg8eyjkjKD0ZzNRJXcPyittPE6/nGbs1/ByxCqnAoz+EZeX6x6Qaz6eocbjvTuls\\nFEcX1MAbPxL+8n7HMKoGI4JpPTTNwkcR0gzBAnNraiqzr9h1WEyeEhL2sakSiw+d\\nXyPGE7WpAgMBAAECggEABEfbixBjnDwYzd7OnOs1teX3yzxR+VlRfBoP9fdJ4Tfc\\nLfHE49zOGzzrYkrx5zhKA9MbvW8sOOEYZ07nnpdtr0e4q8ae8hBueE/Mj9Fur7jI\\ngKdgN7q1LBWtI2lZIAmsqg4x49k0/KurP0cH6D16LgONR5ynIZoxuozT+KWbxTfw\\nvagIdw9DQGfRaMApMC+UGwgnCAgfZHaIbeaW6qLCcK6Jbasjby65E6RaFZBuKQK/\\nBr5Q15LChrn+dr06FZhiaff7h3Mbw1gZv5CWS+hlrlqmrGYeB4/A/F2QWlCVdKNu\\nTI5TY1NyNhRKZDzB78CQbTrM9iVQkh6pMTwWcaponQKBgQDmNaJXOr9u4PnqbvA1\\nwt8YQhb5U3pQaxtq83+suZuKlwDLuET95XiYfnRTWBA1Br9XFzzabD4cREBOMn1w\\nITFD67xkh6pyl/vZ6zr/3tXsarERg1C5zHHnxyzsmI+C+QGafipc4wsdRQNyDNKn\\nRq0tfqXKcUT/fmJibj2iBuW87QKBgQDJkND/d4n81Kg+8in6aYuBPvaT46MibVVM\\nD8dycXnC8WIu+s4oSyRE0tVd7Ov0tEoCniHdhCwYzCXgyFhiMlSV9rDEHGn0BTmn\\n+d35RzAaEVegIs38HHtEZaivuHUqVKBuY1ZRmsIHnxpE8uyLslPs6ool4OMYaJQi\\nwqn5iceALQKBgHWiuJccdRAitHJgSC0grEHIG9dCGgGYCcWoMfjAS8QRD54KRm77\\nYJmEHMst9/IwxXuqazUuFLr7AWU0FbdDrtoHTxiL3sR5939O3bI8W1JY3qyVTp3y\\n483NkJ5CAyupNHGOwPUALADD8FPKS6agzgOucOe248wu9/VWYfY20hStAoGBAJxO\\n5yQVCTa9mrMljBQaoQpbT1AkKS/bZVgUrHA9O1VHZwJTkGeCzd9pN/kcb2ZWIUai\\nZlSn2KJqPb1w38lWim243h9WgXuwSaPPiqly4QR2CmOuWdp2I8lIpGWQP5vSAI9a\\nDWg/ANR0NyTc1T8HzTleMD9rg/A7uRSzrKhMhh8JAoGAJR8RJDzfTuDs/IXuwO4n\\ng4Mi0alpP5UUYvNZDVN60yLDljxL/exavfLMog50s4YGfOUKfhc4aWanwxXhqEGN\\n/rIweYDIrPhWbQr/fzjT8nNcEu+BgG5L1U2qrovIcA1zhQdsL6Jldsx54mD/swq5\\nrjUGJIAF3v0uAWLTzXAZhlA=\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-miid3@trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"client_id\": \"106629813716198798658\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-miid3%40trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n"
                        )  });
            }
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromJson(
            //        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"ordefi-ac5e1\",\r\n  \"private_key_id\": \"e2acf85dc7146e44ff594e60c3ab09f3ddfaeed0\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCwWCb2zajxAFbz\\nHQID3AxGZLj7jARYiuKl2qsL0doP+cf3ZqLKeQLxfPpGbFbQxyThDZZEasQD1yqr\\n4DzKRLxiq5tXMgfNYBEZKlxv/NXPASSB1OGR2DoeP9njOniIJ1v+JlKFo4z2wtNA\\njAjIJ/BJuY8OHMDkx9BAnPbjtK1VUAtyNrEZeki3YXVguqNpzlvq7bKhmlzudURp\\nRVEyl+ogXC8CM90+j3IThTQDu09cPv3ZQueF7pEaUbgjkcX9AjjfIDeyCE79oleT\\nobwXljsffsxZ6YRaTSIy9W6f7Tew/W6xf9gPpGKAa3OA8QwciPDOgqZb/mMImsqW\\npxWA6MCPAgMBAAECggEAFwvA39fwdbqSd8g5N1w+exlLtuLNaJg8rD81GQuDq5B8\\nX9VD03aCz3YtVnRvLhlwijxBK098BCJ5EFLzBDN9HooIeESbZ22KySv+PkvKfM33\\nQZkazPl3XZ+rmo94KWxiEIddQ9t5dIBGpR9dQHHpFxbPNCFxhhSk9I9u5phPX3V+\\n17wBDzZ2gQTS8cWQzoBMu2AduotZV/m8d4K5/W37VsntLWhWBsjtVBRQDKp3fpji\\nKJmzHUBi8EbapIMF7Xnqb++3yRWLZ/AlENVjKYIjfIs4PKEQ9z/iLoyb6nSlVLR+\\nl3efy+gmypgE/OEQkd4aGSlAVLES4OSfizBZUslUnQKBgQDtKUKSW7Y+jHiG3zRS\\nm+NjbzZOLZvGEA01OPj/Tr5bLn2POoPEFBRkx2S9/4aal4xTee65l5TqAz04fFHT\\nUMzN8EzIgXHTnyAqVdmS4D+wN91gmcZ0635Qryv4D9l2rXY+3TKb4dQyP+jgq9kP\\nF9bJG1teqHTJ+6MaH0ou8dyFJQKBgQC+Wiom3WImShMzpJMyHFSIshcYksnaNJnV\\nE2YlAEVRSg5ecPKa0t2QnMjqUVh791Me251CGmH00sS5vOjtDa78jgdQ7/n3CH/d\\nefdTt6fF3JhYkiSEXeFAnyWxl8v8PBHQ8hmoVXLP7iE8RR0WCHT2RPb2s7g9E3+V\\npqBrxyfyowKBgBimnUDRP5Qoza5XLP0ZLv165c4id3qS2Iybq2X+LVMU9gAZOPD8\\nlVIUV6hYEwmmjvNuTR4FX3kjdQ2V/ntUfrU5MQH2UnqIo7NC0SavIG+UnvIXicVF\\nZQ/F5XAXVac8SHooS8ZgJspCUlZoBlvHQpAMw+aG4/zKpx1j+zX99cnxAoGAB1J+\\nlpojgWeOD4mAJAhU9CEtpENZQmyNklW5syisgfEAVn+vxWbhIQP28pSIe9RKvUAj\\nb3yGUh2ddaufpibFmW95NpLUob0Q9hsP+YdyC7ltntKYVwMpfAvjLk6xiRVidhAq\\nDoCi9v6pBqF9+e//V83VVi4ZozfRy5bqStsRXVMCgYA5ZH8vOeURA0JteeNOFB07\\n1KSWOuKbCzh0Sp1ddlM7byMCgTehmkaqfANdoCyHEqNJDkYHtdcrKquFjKFWgunr\\n4a0JJ3brS1/aIl7xmEFgDSZ+d9L0BKbQeXJfJdA1Un69NGYuMjHeaPvSdHBTsYYl\\nUPIxxS9C/zIAETAU7bbkcQ==\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-dmw2t@ordefi-ac5e1.iam.gserviceaccount.com\",\r\n  \"client_id\": \"100662905956027114807\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-dmw2t%40ordefi-ac5e1.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n")
            //});

            var registirationTokens = registrationTokens;
            var message = new MulticastMessage()
            {
                Tokens = registirationTokens,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Body = body,
                    Title = title,
                    
                }
            };
            return await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);

        }

        public Task<FirebaseAdmin.Messaging.BatchResponse> SendNotificationEmployee(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<FirebaseAdmin.Messaging.BatchResponse> SendNotificationOrderReadyToUser(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<BatchResponse> SendNotificationSeller(int orderId)
        {

            var app = FirebaseApp.DefaultInstance;
            if (FirebaseApp.DefaultInstance == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(
                        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"trendimaa-57b1c\",\r\n  \"private_key_id\": \"5338722526999e67bba4826deb49cd33859db66f\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC1QlaTHQPy4oYN\\nanlxJYayKrSRvMyL+llNtIEu5CVMlEPymA8n73RoibeBsA+TMpw9jLnV5BKy7yJt\\ny3kdU0iv44r6Yh6hPvtzSbTtWZtg9ieFQIbRzKBnrn52wYaTlmy07QLcP3ymqu3B\\ncQqBp9PrR8qaOcPrNpiKXh3u+hK+lXdOf0SEgh9H0YS7y5IwmHbJv4iyfRRVLpPv\\nzg8eyjkjKD0ZzNRJXcPyittPE6/nGbs1/ByxCqnAoz+EZeX6x6Qaz6eocbjvTuls\\nFEcX1MAbPxL+8n7HMKoGI4JpPTTNwkcR0gzBAnNraiqzr9h1WEyeEhL2sakSiw+d\\nXyPGE7WpAgMBAAECggEABEfbixBjnDwYzd7OnOs1teX3yzxR+VlRfBoP9fdJ4Tfc\\nLfHE49zOGzzrYkrx5zhKA9MbvW8sOOEYZ07nnpdtr0e4q8ae8hBueE/Mj9Fur7jI\\ngKdgN7q1LBWtI2lZIAmsqg4x49k0/KurP0cH6D16LgONR5ynIZoxuozT+KWbxTfw\\nvagIdw9DQGfRaMApMC+UGwgnCAgfZHaIbeaW6qLCcK6Jbasjby65E6RaFZBuKQK/\\nBr5Q15LChrn+dr06FZhiaff7h3Mbw1gZv5CWS+hlrlqmrGYeB4/A/F2QWlCVdKNu\\nTI5TY1NyNhRKZDzB78CQbTrM9iVQkh6pMTwWcaponQKBgQDmNaJXOr9u4PnqbvA1\\nwt8YQhb5U3pQaxtq83+suZuKlwDLuET95XiYfnRTWBA1Br9XFzzabD4cREBOMn1w\\nITFD67xkh6pyl/vZ6zr/3tXsarERg1C5zHHnxyzsmI+C+QGafipc4wsdRQNyDNKn\\nRq0tfqXKcUT/fmJibj2iBuW87QKBgQDJkND/d4n81Kg+8in6aYuBPvaT46MibVVM\\nD8dycXnC8WIu+s4oSyRE0tVd7Ov0tEoCniHdhCwYzCXgyFhiMlSV9rDEHGn0BTmn\\n+d35RzAaEVegIs38HHtEZaivuHUqVKBuY1ZRmsIHnxpE8uyLslPs6ool4OMYaJQi\\nwqn5iceALQKBgHWiuJccdRAitHJgSC0grEHIG9dCGgGYCcWoMfjAS8QRD54KRm77\\nYJmEHMst9/IwxXuqazUuFLr7AWU0FbdDrtoHTxiL3sR5939O3bI8W1JY3qyVTp3y\\n483NkJ5CAyupNHGOwPUALADD8FPKS6agzgOucOe248wu9/VWYfY20hStAoGBAJxO\\n5yQVCTa9mrMljBQaoQpbT1AkKS/bZVgUrHA9O1VHZwJTkGeCzd9pN/kcb2ZWIUai\\nZlSn2KJqPb1w38lWim243h9WgXuwSaPPiqly4QR2CmOuWdp2I8lIpGWQP5vSAI9a\\nDWg/ANR0NyTc1T8HzTleMD9rg/A7uRSzrKhMhh8JAoGAJR8RJDzfTuDs/IXuwO4n\\ng4Mi0alpP5UUYvNZDVN60yLDljxL/exavfLMog50s4YGfOUKfhc4aWanwxXhqEGN\\n/rIweYDIrPhWbQr/fzjT8nNcEu+BgG5L1U2qrovIcA1zhQdsL6Jldsx54mD/swq5\\nrjUGJIAF3v0uAWLTzXAZhlA=\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-miid3@trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"client_id\": \"106629813716198798658\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-miid3%40trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n"
                        )
                });
            }

            string body="";
            string title="";
            var order = await _context.Orders.Where(i => i.Id == orderId).FirstOrDefaultAsync(i=>i.Id==orderId);
            var sellerkeys=  _context.Orders.Where(i=>i.Id==orderId).Select(i=>i.Seller).SelectMany(i=>i.UserKeys.Select(i=>i.Key)).ToList();
            var lang =await _context.Orders.Where(i => i.Id == orderId).Select(i => i.AppUser.Language).FirstOrDefaultAsync();
            if (lang==Common.Enum.Language.FI)
                 title = "Uusi tilaus saapui🚚";  body = $"{order.Name} {order.Surname} on vastaanottanut uuden tilausnumeron {order.OrderNumber}.";
            if (lang == Common.Enum.Language.ENG)
                title = "New order has came"; body = $"{order.Name} {order.Surname} has received a new order number  {order.OrderNumber}.";
            if (lang == Common.Enum.Language.SV)
                title = "Ny beställning har kommit🚚"; body = $"{order.Name} {order.Surname} har mottagit en ny beställningsnummer  {order.OrderNumber}.";
            if (lang == Common.Enum.Language.NO)
                title = "Ny bestilling har kommet🚚"; body = $"{order.Name} {order.Surname} har mottatt en ny bestillingsnummer  {order.OrderNumber}.";
            if (lang == Common.Enum.Language.AR)
                title = "العنوان\": \"وصل طلب جديد🚚"; body = $"{order.Name} {order.Surname} has received a new order number  {order.OrderNumber}.";
            if (lang == Common.Enum.Language.DA)
                title = "Ny ordre er ankommet🚚"; body = $"{order.Name} {order.Surname} har modtaget en ny ordrenummer  {order.OrderNumber}.";
            sellerkeys=sellerkeys.Distinct().ToList();
            
            var message = new MulticastMessage()
            {
                Tokens = sellerkeys,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Body = body,
                    Title = title,

                }
            };
            var data = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            return data;
        }


        public async Task<string> SendNotificationSingle(string registrationToken, string title, string body)
        {
            var app = FirebaseApp.DefaultInstance;
            if (FirebaseApp.DefaultInstance == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(
                        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"trendimaa-57b1c\",\r\n  \"private_key_id\": \"5338722526999e67bba4826deb49cd33859db66f\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC1QlaTHQPy4oYN\\nanlxJYayKrSRvMyL+llNtIEu5CVMlEPymA8n73RoibeBsA+TMpw9jLnV5BKy7yJt\\ny3kdU0iv44r6Yh6hPvtzSbTtWZtg9ieFQIbRzKBnrn52wYaTlmy07QLcP3ymqu3B\\ncQqBp9PrR8qaOcPrNpiKXh3u+hK+lXdOf0SEgh9H0YS7y5IwmHbJv4iyfRRVLpPv\\nzg8eyjkjKD0ZzNRJXcPyittPE6/nGbs1/ByxCqnAoz+EZeX6x6Qaz6eocbjvTuls\\nFEcX1MAbPxL+8n7HMKoGI4JpPTTNwkcR0gzBAnNraiqzr9h1WEyeEhL2sakSiw+d\\nXyPGE7WpAgMBAAECggEABEfbixBjnDwYzd7OnOs1teX3yzxR+VlRfBoP9fdJ4Tfc\\nLfHE49zOGzzrYkrx5zhKA9MbvW8sOOEYZ07nnpdtr0e4q8ae8hBueE/Mj9Fur7jI\\ngKdgN7q1LBWtI2lZIAmsqg4x49k0/KurP0cH6D16LgONR5ynIZoxuozT+KWbxTfw\\nvagIdw9DQGfRaMApMC+UGwgnCAgfZHaIbeaW6qLCcK6Jbasjby65E6RaFZBuKQK/\\nBr5Q15LChrn+dr06FZhiaff7h3Mbw1gZv5CWS+hlrlqmrGYeB4/A/F2QWlCVdKNu\\nTI5TY1NyNhRKZDzB78CQbTrM9iVQkh6pMTwWcaponQKBgQDmNaJXOr9u4PnqbvA1\\nwt8YQhb5U3pQaxtq83+suZuKlwDLuET95XiYfnRTWBA1Br9XFzzabD4cREBOMn1w\\nITFD67xkh6pyl/vZ6zr/3tXsarERg1C5zHHnxyzsmI+C+QGafipc4wsdRQNyDNKn\\nRq0tfqXKcUT/fmJibj2iBuW87QKBgQDJkND/d4n81Kg+8in6aYuBPvaT46MibVVM\\nD8dycXnC8WIu+s4oSyRE0tVd7Ov0tEoCniHdhCwYzCXgyFhiMlSV9rDEHGn0BTmn\\n+d35RzAaEVegIs38HHtEZaivuHUqVKBuY1ZRmsIHnxpE8uyLslPs6ool4OMYaJQi\\nwqn5iceALQKBgHWiuJccdRAitHJgSC0grEHIG9dCGgGYCcWoMfjAS8QRD54KRm77\\nYJmEHMst9/IwxXuqazUuFLr7AWU0FbdDrtoHTxiL3sR5939O3bI8W1JY3qyVTp3y\\n483NkJ5CAyupNHGOwPUALADD8FPKS6agzgOucOe248wu9/VWYfY20hStAoGBAJxO\\n5yQVCTa9mrMljBQaoQpbT1AkKS/bZVgUrHA9O1VHZwJTkGeCzd9pN/kcb2ZWIUai\\nZlSn2KJqPb1w38lWim243h9WgXuwSaPPiqly4QR2CmOuWdp2I8lIpGWQP5vSAI9a\\nDWg/ANR0NyTc1T8HzTleMD9rg/A7uRSzrKhMhh8JAoGAJR8RJDzfTuDs/IXuwO4n\\ng4Mi0alpP5UUYvNZDVN60yLDljxL/exavfLMog50s4YGfOUKfhc4aWanwxXhqEGN\\n/rIweYDIrPhWbQr/fzjT8nNcEu+BgG5L1U2qrovIcA1zhQdsL6Jldsx54mD/swq5\\nrjUGJIAF3v0uAWLTzXAZhlA=\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-miid3@trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"client_id\": \"106629813716198798658\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-miid3%40trendimaa-57b1c.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n"
                        )
                });
            }
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromJson(
            //        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"ordefi-ac5e1\",\r\n  \"private_key_id\": \"e2acf85dc7146e44ff594e60c3ab09f3ddfaeed0\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCwWCb2zajxAFbz\\nHQID3AxGZLj7jARYiuKl2qsL0doP+cf3ZqLKeQLxfPpGbFbQxyThDZZEasQD1yqr\\n4DzKRLxiq5tXMgfNYBEZKlxv/NXPASSB1OGR2DoeP9njOniIJ1v+JlKFo4z2wtNA\\njAjIJ/BJuY8OHMDkx9BAnPbjtK1VUAtyNrEZeki3YXVguqNpzlvq7bKhmlzudURp\\nRVEyl+ogXC8CM90+j3IThTQDu09cPv3ZQueF7pEaUbgjkcX9AjjfIDeyCE79oleT\\nobwXljsffsxZ6YRaTSIy9W6f7Tew/W6xf9gPpGKAa3OA8QwciPDOgqZb/mMImsqW\\npxWA6MCPAgMBAAECggEAFwvA39fwdbqSd8g5N1w+exlLtuLNaJg8rD81GQuDq5B8\\nX9VD03aCz3YtVnRvLhlwijxBK098BCJ5EFLzBDN9HooIeESbZ22KySv+PkvKfM33\\nQZkazPl3XZ+rmo94KWxiEIddQ9t5dIBGpR9dQHHpFxbPNCFxhhSk9I9u5phPX3V+\\n17wBDzZ2gQTS8cWQzoBMu2AduotZV/m8d4K5/W37VsntLWhWBsjtVBRQDKp3fpji\\nKJmzHUBi8EbapIMF7Xnqb++3yRWLZ/AlENVjKYIjfIs4PKEQ9z/iLoyb6nSlVLR+\\nl3efy+gmypgE/OEQkd4aGSlAVLES4OSfizBZUslUnQKBgQDtKUKSW7Y+jHiG3zRS\\nm+NjbzZOLZvGEA01OPj/Tr5bLn2POoPEFBRkx2S9/4aal4xTee65l5TqAz04fFHT\\nUMzN8EzIgXHTnyAqVdmS4D+wN91gmcZ0635Qryv4D9l2rXY+3TKb4dQyP+jgq9kP\\nF9bJG1teqHTJ+6MaH0ou8dyFJQKBgQC+Wiom3WImShMzpJMyHFSIshcYksnaNJnV\\nE2YlAEVRSg5ecPKa0t2QnMjqUVh791Me251CGmH00sS5vOjtDa78jgdQ7/n3CH/d\\nefdTt6fF3JhYkiSEXeFAnyWxl8v8PBHQ8hmoVXLP7iE8RR0WCHT2RPb2s7g9E3+V\\npqBrxyfyowKBgBimnUDRP5Qoza5XLP0ZLv165c4id3qS2Iybq2X+LVMU9gAZOPD8\\nlVIUV6hYEwmmjvNuTR4FX3kjdQ2V/ntUfrU5MQH2UnqIo7NC0SavIG+UnvIXicVF\\nZQ/F5XAXVac8SHooS8ZgJspCUlZoBlvHQpAMw+aG4/zKpx1j+zX99cnxAoGAB1J+\\nlpojgWeOD4mAJAhU9CEtpENZQmyNklW5syisgfEAVn+vxWbhIQP28pSIe9RKvUAj\\nb3yGUh2ddaufpibFmW95NpLUob0Q9hsP+YdyC7ltntKYVwMpfAvjLk6xiRVidhAq\\nDoCi9v6pBqF9+e//V83VVi4ZozfRy5bqStsRXVMCgYA5ZH8vOeURA0JteeNOFB07\\n1KSWOuKbCzh0Sp1ddlM7byMCgTehmkaqfANdoCyHEqNJDkYHtdcrKquFjKFWgunr\\n4a0JJ3brS1/aIl7xmEFgDSZ+d9L0BKbQeXJfJdA1Un69NGYuMjHeaPvSdHBTsYYl\\nUPIxxS9C/zIAETAU7bbkcQ==\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-dmw2t@ordefi-ac5e1.iam.gserviceaccount.com\",\r\n  \"client_id\": \"100662905956027114807\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-dmw2t%40ordefi-ac5e1.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n")
            //});

            var registirationToken = registrationToken;
            var message = new Message()
            {
                Token = registirationToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Body = body,
                    Title = title,

                }
            };
            var data= await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return data;
        }

        public async Task<BatchResponse> SendNotificationTestSeller(int sellerId, string title, string body)
        {
           var sellerKeys=await _context.Sellers.Where(i=>i.Id==sellerId).SelectMany(i=>i.UserKeys.Select(i=>i.Key)).ToListAsync();
          var response= await SendNotification(sellerKeys, title,body);
            return response;
        }
    }
}
