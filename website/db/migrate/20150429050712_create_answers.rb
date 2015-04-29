class CreateAnswers < ActiveRecord::Migration
  def change
    create_table :answers do |t|
      t.belongs_to :question
      t.belongs_to :student
      t.string :ans
      t.boolean :correct
      t.timestamps null: false
    end
  end
end
