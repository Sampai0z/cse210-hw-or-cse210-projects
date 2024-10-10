using System;
using System.Collections.Generic;

namespace Foundation1
{
    // Define the Comment class
    public class Comment
    {
        // Private fields
        private string _commenter;
        private string _text;

        // Constructor
        public Comment(string commenter, string text)
        {
            _commenter = commenter;
            _text = text;
        }

        // Public properties
        public string Commenter
        {
            get { return _commenter; }
        }

        public string Text
        {
            get { return _text; }
        }

        // Method to display the comment
        public void DisplayComment()
        {
            Console.WriteLine($"{_commenter}: {_text}");
        }
    }

    // Define the Video class
    public class Video
    {
        // Private fields
        private string _title;
        private string _author;
        private int _length;  // in seconds
        private List<Comment> _comments;

        // Constructor
        public Video(string title, string author, int length)
        {
            _title = title;
            _author = author;
            _length = length;
            _comments = new List<Comment>();
        }

        // Public properties
        public string Title
        {
            get { return _title; }
        }

        public string Author
        {
            get { return _author; }
        }

        public int Length
        {
            get { return _length; }
        }

        // Method to add a comment
        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        // Method to return the number of comments
        public int GetCommentCount()
        {
            return _comments.Count;
        }

        // Method to display the video details and comments
        public void DisplayVideoDetails()
        {
            Console.WriteLine($"Title: {_title}");
            Console.WriteLine($"Author: {_author}");
            Console.WriteLine($"Length: {_length} seconds");
            Console.WriteLine($"Number of Comments: {GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in _comments)
            {
                comment.DisplayComment();
            }
            Console.WriteLine();  // Newline for spacing
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            // Create video objects
            Video video1 = new Video("Understanding C# Classes", "John Doe", 600);
            Video video2 = new Video("Introduction to Java", "Jane Smith", 720);
            Video video3 = new Video("Learning HTML & CSS", "Alice Brown", 480);

            // Add comments to the videos
            video1.AddComment(new Comment("Chris", "Great explanation!"));
            video1.AddComment(new Comment("Sam", "Very helpful video."));
            video1.AddComment(new Comment("Jamie", "I learned a lot, thanks!"));

            video2.AddComment(new Comment("Alex", "This was very informative!"));
            video2.AddComment(new Comment("Taylor", "Java is tough, but this helped!"));

            video3.AddComment(new Comment("Jordan", "Clear and concise."));
            video3.AddComment(new Comment("Morgan", "Could you make a follow-up on CSS Grid?"));
            video3.AddComment(new Comment("Lee", "Awesome tutorial!"));

            // Store the videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Display details for each video
            foreach (var video in videos)
            {
                video.DisplayVideoDetails();
            }
        }
    }
}
