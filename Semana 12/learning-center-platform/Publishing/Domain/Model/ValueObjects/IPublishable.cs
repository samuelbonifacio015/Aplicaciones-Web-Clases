namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;
/// <summary>
///     Represents a publishable content item in the ACME Learning Center Platform.
/// </summary>
public interface IPublishable
{
    void SendToEdit();
    void SendToApproval();
    void ApprovedAndLock();
    void Reject();
    void ReturnToEdit();
}