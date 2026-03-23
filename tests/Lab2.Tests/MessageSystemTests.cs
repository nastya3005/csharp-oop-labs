using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using NSubstitute;
using NUnit.Framework;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

[TestFixture]
public class MessageSystemTests
{
    [Test]
    public void TestCase1_MessageReceived_ShouldBeInUnreadStatus()
    {
        var user = new User("TestUser");
        var message = new Message("Test", "Body", 2);

        user.Recieve(message);

        Assert.That(user.IsMessageRead(message), Is.False);
    }

    [Test]
    public void TestCase2_MarkUnreadMessageAsRead_ShouldChangeStatus()
    {
        var user = new User("TestUser");
        var message = new Message("Test", "Body", 2);
        user.Recieve(message);

        Assert.That(user.IsMessageRead(message), Is.False);

        user.MarkAsRead(message);

        Assert.That(user.IsMessageRead(message), Is.True);
    }

    [Test]
    public void TestCase3_MarkAlreadyReadMessageAsRead_ShouldNotChangeStatus()
    {
        var user = new User("TestUser");
        var message = new Message("Test", "Body", 2);
        user.Recieve(message);
        user.MarkAsRead(message);

        Assert.Throws<ArgumentException>(() => user.MarkAsRead(message));
    }

    [Test]
    public void TestCase4_FilteredRecipient_WithUnsuitablePriority_ShouldNotReceiveMessage()
    {
        IRecipient mockRecipient = Substitute.For<IRecipient>();
        var filter = new ImportanceFilter(mockRecipient, minPriority: 3, maxPriority: 4);
        var lowPriorityMessage = new Message("Test", "Body", 1);

        filter.Recieve(lowPriorityMessage);

        mockRecipient.DidNotReceive().Recieve(lowPriorityMessage);
    }

    [Test]
    public void TestCase5_LoggedRecipient_ShouldWriteLogWhenMessageReceived()
    {
        IRecipient mockRecipient = Substitute.For<IRecipient>();
        ILogger mockLogger = Substitute.For<ILogger>();
        var loggingDecorator = new LoggingDecorator(mockRecipient, mockLogger);
        var message = new Message("Test", "Body", 2);

        loggingDecorator.Recieve(message);

        mockLogger.Received(1).Log(mockRecipient, message);
        mockRecipient.Received(1).Recieve(message);
    }

    [Test]
    public void TestCase6_FormattingArchiver_ShouldCallFormatterAndSaveRaw()
    {
        IArchiver mockArchiver = Substitute.For<IArchiver>();
        IFormatter mockFormatter = Substitute.For<IFormatter>();
        var formattingArchiver = new FormattingArchiver(mockArchiver, mockFormatter);
        var message = new Message("Test", "Body", 2);

        mockFormatter.Format(message).Returns("Formatted Message");

        formattingArchiver.Save(message);

        mockFormatter.Received(1).Format(message);
        mockArchiver.Received(1).SaveRaw("Formatted Message");
    }

    [Test]
    public void TestCase7_TwoRecipientsWithDifferentFilters_UserShouldReceiveMessageOnce()
    {
        IUser mockUser = Substitute.For<IUser>();
        var topic = new Topic("TestTopic");

        topic.AddRecipient(mockUser); // Первый адресат

        var filteredUser = new ImportanceFilter(mockUser, minPriority: 3, maxPriority: 4);
        topic.AddRecipient(filteredUser); // Второй адресат с фильтром

        var lowPriorityMessage = new Message("Test", "Body", 1);

        topic.Recieve(lowPriorityMessage);

        mockUser.Received(1).Recieve(lowPriorityMessage);
    }
}