class CreateStudentAnswerQuestions < ActiveRecord::Migration
  def change
    create_table :student_answer_questions do |t|
      t.string :answer
      t.belongs_to :student
      t.belongs_to :question
      t.boolean :correct

      t.timestamps null: false
    end
  end
end
