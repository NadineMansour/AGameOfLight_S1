class CreateStudents < ActiveRecord::Migration
  def change
    create_table :students do |t|
    	t.string :school
    	t.string :student_class
    	t.string :grade
    	t.string :student_name
        t.timestamps null: false
    end
  end
end

#extrenal documentation
#create_students.rb creates a table Student which has the attributes school, student_class, grade and student_name
