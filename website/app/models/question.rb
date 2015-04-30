class Question < ActiveRecord::Base
  belongs_to :teacher
  belongs_to :subject
  has_many :student_answer_questions


 def shuffled_answers
    answers = [correct_answer, wrong_answer_one, wrong_answer_two, wrong_answer_three]
    answers.shuffle
  end

end
