using DotnetSdk.Common.Models;
using DotnetSdk.Sdk.Client;

string endpoint = "http://localhost:5149";
string apikey = "5mJk7RSBqhENCznb1ZLRj7F8e1rHONZf";

var client = AssessReviewClient.Create(endpoint, apikey);

var reviews = new ReviewRequest[]
{
    // Very good review
    new("Absolutely fantastic experience! The tasting menu was exquisite, with each course perfectly crafted. The staff went above and beyond to make our night memorable. Highly recommended!"),
    
    // Good reviews
    new("The pizza was absolutely delicious! Perfectly cheesy and crispy."),
    new("Fantastic dining experience, with prompt service and a cozy atmosphere."),
    new("The seafood platter was fresh and full of flavor. Highly recommended!"),
    
    // Medium reviews
    new("The steak was good, but a bit overcooked for my taste. Sides were nice."),
    new("The brunch menu had great options, but the wait time was longer than expected."),
    new("The pasta was decent, but it lacked seasoning. The ambiance was nice though."),

    // Bad reviews
    new("The salad felt stale and lacked flavor. I expected better for the price."),
    new("Service was quite slow, and the main course arrived lukewarm."),
    new("The steak was tough, and it took forever to get our drinks refilled."),

    // Very bad reviews
    new("Terrible experience! The food was cold, and the service was nonexistent."),
    new("The fish tasted off, and I left feeling sick. I won't be coming back."),
};


foreach (var review in reviews)
{
    await client.CreateReviewAsync(review);
}

int page = 1;
bool hasNextPage;
do
{
    var paginatedReviews = await client.ListAssessReviewsAsync(page);
    Console.WriteLine($"Page {page} of Reviews:");

    foreach (var review in paginatedReviews.Data)
    {
        Console.WriteLine($"Message: {review.Message} - Type: {review.Description}");
    }

    hasNextPage = paginatedReviews.HasNextPage;
    page++;

} while (hasNextPage);