class CreateQuestions < ActiveRecord::Migration
  def change
    create_table :questions do |t|
      t.string :body
      t.string :correct_answer
      t.string :wrong_answer_one
      t.string :wrong_answer_two
      t.string :wrong_answer_three
      t.references :subject
      t.references :teacher
      t.timestamps null: false
    end
  end
end
