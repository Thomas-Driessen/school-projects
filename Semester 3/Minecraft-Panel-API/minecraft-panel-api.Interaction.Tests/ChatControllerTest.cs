using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using minecraft_panel_api.Interaction.Controllers;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;
using minecraft_panel_api.Interaction.Hubs;
using Moq;
using Xunit;

namespace minecraft_panel_api.Interaction.Tests
{
    public class ChatControllerTest
    {
        // Check if player data returns the expected Player
        [Theory]
        [MemberData(nameof(TestGetChatMessagesWithAmountDataSet))]
        public async void TestGetChatMessagesWithAmount(Pagination pagination, List<ChatMessage> chatMessages)
        {
            //Arange
            var socketHubMock = new Mock<IHubContext<SocketTestHub>>();
            ChatDbAccessMock chatDbAccess = new ChatDbAccessMock();
            ChatController chatController = new ChatController(socketHubMock.Object, chatDbAccess);
            
            //Act
            ActionResult<List<ChatMessage>> result = await chatController.GetChatMessagesWithAmount(pagination);
            
            //Assert
            Assert.Equal(chatMessages, result.Value);
        }

        public static IEnumerable<object[]> TestGetChatMessagesWithAmountDataSet()
        {
            List<object[]> result = new List<object[]>();

            Pagination pagination = new Pagination
            {
                Skip = 0,
                Take = 9
            };

            List<ChatMessage> chatMessages = new List<ChatMessage>();
            for (int i = 0; i < 10; i++)
            {
                chatMessages.Add(new ChatMessage
                {
                    Content = "TEST",
                    SenderName = "TEST USER",
                    TimeStamp = DateTime.Today
                });
            }
            
            result.Add(new object[] { pagination, chatMessages });

            return result;
        }
        
        [Theory]
        [MemberData(nameof(TestSaveChatMessageDataSet))]
        public async void TestSaveChatMessage(ChatMessage chatMessage, string expectedResult)
        {
            //Arange
            var socketHubMock = new Mock<IHubContext<SocketTestHub>>();
            ChatDbAccessMock chatDbAccess = new ChatDbAccessMock();
            ChatController chatController = new ChatController(socketHubMock.Object, chatDbAccess);
            
            //Act
            ActionResult<string> result = await chatController.SaveChatMessage(chatMessage);
            
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }

        public static IEnumerable<object[]> TestSaveChatMessageDataSet()
        {
            List<object[]> result = new List<object[]>();
            
            ChatMessage chatMessage = new ChatMessage
            {
                Content = "TEST",
                SenderName = "TEST USER",
                TimeStamp = DateTime.Today
            };

            string expectedResult = "Message saved";
            
            result.Add(new object[] { chatMessage, expectedResult });

            return result;
        }
        
        [Theory]
        [MemberData(nameof(TestGetTotalMessageCountDataSet))]
        public async void TestGetTotalMessageCount(int expectedCount)
        {
            //Arange
            var socketHubMock = new Mock<IHubContext<SocketTestHub>>();
            ChatDbAccessMock chatDbAccess = new ChatDbAccessMock();
            ChatController chatController = new ChatController(socketHubMock.Object, chatDbAccess);
            
            //Act
            int result = await chatController.GetTotalMessageCount();
            
            //Assert
            Assert.Equal(expectedCount, result);
        }

        public static IEnumerable<object[]> TestGetTotalMessageCountDataSet()
        {
            List<object[]> result = new List<object[]>();

            int expectedCount = 50;
            
            result.Add(new object[] { expectedCount });

            return result;
        }

        public class ChatDbAccessMock : IChatDbAccess
        {
            public async Task<List<ChatMessage>> GetChatMessagesWithAmount(Pagination pagination)
            {
                List<ChatMessage> chatMessages = new List<ChatMessage>();
                for (int i = 0; i < 10; i++)
                {
                    chatMessages.Add(new ChatMessage
                    {
                        Content = "TEST",
                        SenderName = "TEST USER",
                        TimeStamp = DateTime.Today
                    });
                }

                return chatMessages;
            }

            public async Task<int> GetTotalMessageCount()
            {
                return 50;
            }

            public async Task<int> SaveChatMessage(ChatMessage chatMessage)
            {
                return 1;
            }
        }
    }
}