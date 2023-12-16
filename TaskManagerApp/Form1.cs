using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();

            taskPanel = new Panel
            {
                AutoSize = true,
                Location = new Point(303 + 40, 120),
            };

            // Add the panel to the form's controls
            this.Controls.Add(taskPanel);
        }

        // Class that sets the text and priority
        public class TaskItem
        {
            public string Task { get; set; }
            public string Priority { get; set; }
        }

        // Storing the tasks in a list
        private List<TaskItem> tasks = new List<TaskItem>();

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void submitBtn_Click(object sender, EventArgs e)
        {
            // Retrieve text from text box
            string taskRequired = textBox1.Text.Trim();

            // Check if the user has entered a task
            if (!string.IsNullOrEmpty(taskRequired))
            {
                // Retrieving the priority of the task
                string priority = GetSelectedPriority();

                // Creating a new checkbox for the task
                CheckBox checkBox = CreateCheckBox(taskRequired);

                // Adding the checkbox to the panel
                taskPanel.Controls.Add(checkBox);

                // Clear the text box for the next task
                textBox1.Clear();

                // Adding the task according to the priority
                tasks.Add(new TaskItem { Task = taskRequired, Priority = priority });

                // Displaying the priority as a popup
                ShowPriorityPopup(taskRequired, priority);

                // Show tasks in the task panel based on priority
                ShowTaskPriority();
            }
            else
            {
                MessageBox.Show("Please enter a task");
            }
        }


        private string GetSelectedPriority()
        {
            // Determine the selected priority from radio buttons
            if (highRadioButton.Checked)
            {
                return "High";
            }
            else if (mediumRadioButton.Checked)
            {
                return "Medium";
            }
            else if (lowRadioButton.Checked)
            {
                return "Low";
            }
            else
            {
                // Default to Medium if no radio button is selected
                return "Medium";
            }
        }

        private void ShowTaskPriority()
        {
            // Clear existing controls in the task panel
            taskPanel.Controls.Clear();

            // Sort tasks first by priority, then by the order in which they were added
            var sortedTasks = tasks.OrderBy(t => t.Priority).ThenBy(t => tasks.IndexOf(t)).ToList();

            // Iterate through sorted tasks and display them
            int yLocation = 0;  // Start from the top of the panel
            foreach (var taskItem in sortedTasks)
            {
                CheckBox checkBox = CreateCheckBox(taskItem.Task);
                checkBox.Location = new Point(0, yLocation);
                yLocation += checkBox.Height + 10; // Adjust the vertical spacing
                taskPanel.Controls.Add(checkBox);
            }
        }

        private CheckBox CreateCheckBox(string taskRequired)
        {
            // Create a new CheckBox for the newly assigned task
            CheckBox checkBox = new CheckBox
            {
                // The text will be updated to the newly assigned task
                Text = taskRequired,

                // The CheckBox will be automatically sized to fit the text
                AutoSize = true,
            };

            // Return the CheckBox
            return checkBox;
        }

        private void ShowPriorityPopup(string taskRequired, string priority)
        {
            // Display the priority as a popup
            MessageBox.Show($"Task: {taskRequired}\nPriority: {priority}");
        }
    }
}
