using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.Helpers
{
    public class BadgeHelper
    {
        public static string GetBadgeClass(PaymentStatus status)
        {
            return status switch
            {
                PaymentStatus.PENDING => "badge bg-warning",
                PaymentStatus.PAID => "badge bg-success",
                PaymentStatus.FAILED => "badge bg-danger",
                PaymentStatus.CANCELLED => "badge bg-secondary",
                _ => "badge badge-secondary"
            };
        }
    }
}
