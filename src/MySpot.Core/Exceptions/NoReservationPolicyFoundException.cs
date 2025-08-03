using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;

public class NoReservationPolicyFoundException(JobTitle jobTitle)
    : CustomException($"No reservation policy for {jobTitle} has not found.")
{
    public JobTitle JobTitle { get; } = jobTitle;
}